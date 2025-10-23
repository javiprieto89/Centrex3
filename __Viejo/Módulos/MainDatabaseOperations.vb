Imports System.Data.SqlClient
Imports System.Windows.Forms

''' <summary>
''' Módulo para manejar operaciones de base de datos específicas del formulario principal
''' </summary>
Module MainDatabaseOperations
    
    ''' <summary>
    ''' Carga los datos del DataGridView principal
    ''' </summary>
    Public Sub LoadMainDataGrid(grid As DataGridView, tableName As String, Optional filters As String = "")
        Try
            Dim sql As String = $"SELECT * FROM {tableName}"
            If Not String.IsNullOrEmpty(filters) Then
                sql += $" WHERE {filters}"
            End If
            sql += " ORDER BY id_" & tableName & " DESC"
            
            LoadDataGrid(grid, sql)
            FormHelper.ConfigureDataGrid(grid, False)
            
        Catch ex As Exception
            ShowError($"Error cargando datos de {tableName}: {ex.Message}", "Error de Carga")
        End Try
    End Sub
    
    ''' <summary>
    ''' Obtiene información de un comprobante
    ''' </summary>
    Public Function GetComprobanteInfo(comprobanteId As String) As comprobante
        Try
            Dim sql As String = "SELECT * FROM comprobantes WHERE id_comprobante = @id"
            Dim parameters As New Dictionary(Of String, Object) From {{"@id", comprobanteId}}
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim comprobante As New comprobante()
                comprobante.id_comprobante = row("id_comprobante").ToString()
                comprobante.comprobante = row("comprobante").ToString()
                comprobante.maxItems = CInt(row("maxItems"))
                comprobante.activo = CBool(row("activo"))
                Return comprobante
            End If
            
            Return Nothing
        Catch ex As Exception
            Throw New Exception($"Error obteniendo información del comprobante: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Obtiene información de un pedido
    ''' </summary>
    Public Function GetPedidoInfo(pedidoId As String) As pedido
        Try
            Dim sql As String = "SELECT * FROM pedidos WHERE id_pedido = @id"
            Dim parameters As New Dictionary(Of String, Object) From {{"@id", pedidoId}}
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim pedido As New pedido()
                pedido.id_pedido = row("id_pedido").ToString()
                pedido.fecha = CDate(row("fecha"))
                pedido.id_cliente = row("id_cliente").ToString()
                pedido.total = CDec(row("total"))
                pedido.activo = CBool(row("activo"))
                Return pedido
            End If
            
            Return Nothing
        Catch ex As Exception
            Throw New Exception($"Error obteniendo información del pedido: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Obtiene información de un cliente
    ''' </summary>
    Public Function GetClienteInfo(clienteId As String) As cliente
        Try
            Dim sql As String = "SELECT * FROM clientes WHERE id_cliente = @id"
            Dim parameters As New Dictionary(Of String, Object) From {{"@id", clienteId}}
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim cliente As New cliente()
                cliente.id_cliente = row("id_cliente").ToString()
                cliente.nombre = row("nombre").ToString()
                cliente.cuit = row("cuit").ToString()
                cliente.activo = CBool(row("activo"))
                Return cliente
            End If
            
            Return Nothing
        Catch ex As Exception
            Throw New Exception($"Error obteniendo información del cliente: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Obtiene información de un item
    ''' </summary>
    Public Function GetItemInfo(itemId As String) As item
        Try
            Dim sql As String = "SELECT * FROM items WHERE id_item = @id"
            Dim parameters As New Dictionary(Of String, Object) From {{"@id", itemId}}
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim item As New item()
                item.id_item = row("id_item").ToString()
                item.descript = row("descripcion").ToString()
                item.precio_lista = CDec(row("precio"))
                item.cantidad = CDec(row("stock_actual"))
                item.activo = CBool(row("activo"))
                Return item
            End If
            
            Return Nothing
        Catch ex As Exception
            Throw New Exception($"Error obteniendo información del item: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Actualiza el estado de un registro
    ''' </summary>
    Public Function UpdateRecordStatus(tableName As String, recordId As String, active As Boolean) As Boolean
        Try
            Dim sql As String = $"UPDATE {tableName} SET activo = @activo WHERE id_{tableName} = @id"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@activo", active},
                {"@id", recordId}
            }
            
            Dim rowsAffected As Integer = ExecuteNonQuery(sql, parameters)
            Return rowsAffected > 0
            
        Catch ex As Exception
            Throw New Exception($"Error actualizando estado del registro: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Elimina un registro (marca como inactivo)
    ''' </summary>
    Public Function DeleteRecord(tableName As String, recordId As String) As Boolean
        Try
            Return UpdateRecordStatus(tableName, recordId, False)
        Catch ex As Exception
            Throw New Exception($"Error eliminando registro: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Restaura un registro (marca como activo)
    ''' </summary>
    Public Function RestoreRecord(tableName As String, recordId As String) As Boolean
        Try
            Return UpdateRecordStatus(tableName, recordId, True)
        Catch ex As Exception
            Throw New Exception($"Error restaurando registro: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Obtiene estadísticas del sistema
    ''' </summary>
    Public Function GetSystemStats() As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)()
            
            ' Contar registros activos
            stats.Add("ClientesActivos", CInt(ExecuteScalar("SELECT COUNT(*) FROM clientes WHERE activo = 1")))
            stats.Add("ProveedoresActivos", CInt(ExecuteScalar("SELECT COUNT(*) FROM proveedores WHERE activo = 1")))
            stats.Add("ItemsActivos", CInt(ExecuteScalar("SELECT COUNT(*) FROM items WHERE activo = 1")))
            stats.Add("PedidosActivos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE activo = 1")))
            stats.Add("UsuariosActivos", CInt(ExecuteScalar("SELECT COUNT(*) FROM usuarios WHERE activo = 1")))
            
            ' Contar registros totales
            stats.Add("TotalClientes", CInt(ExecuteScalar("SELECT COUNT(*) FROM clientes")))
            stats.Add("TotalProveedores", CInt(ExecuteScalar("SELECT COUNT(*) FROM proveedores")))
            stats.Add("TotalItems", CInt(ExecuteScalar("SELECT COUNT(*) FROM items")))
            stats.Add("TotalPedidos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos")))
            stats.Add("TotalUsuarios", CInt(ExecuteScalar("SELECT COUNT(*) FROM usuarios")))
            
            Return stats
        Catch ex As Exception
            Throw New Exception($"Error obteniendo estadísticas del sistema: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Obtiene el último número de comprobante
    ''' </summary>
    Public Function GetLastComprobanteNumber(puntoVenta As Integer, tipoComprobante As Integer) As Integer
        Try
            Dim sql As String = "SELECT ISNULL(MAX(numeroComprobante), 0) FROM pedidos WHERE puntoVenta = @puntoVenta AND id_tipoComprobante = @tipoComprobante"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@puntoVenta", puntoVenta},
                {"@tipoComprobante", tipoComprobante}
            }
            
            Return CInt(ExecuteScalar(sql, parameters))
        Catch ex As Exception
            Throw New Exception($"Error obteniendo último número de comprobante: {ex.Message}", ex)
        End Try
    End Function
    
    ''' <summary>
    ''' Verifica si hay stock suficiente para un item
    ''' </summary>
    Public Function CheckStockAvailability(itemId As String, requiredQuantity As Decimal) As Boolean
        Try
            Dim sql As String = "SELECT stock_actual FROM items WHERE id_item = @id"
            Dim parameters As New Dictionary(Of String, Object) From {{"@id", itemId}}
            Dim currentStock As Object = ExecuteScalar(sql, parameters)
            
            If currentStock Is Nothing Then Return False
            
            Return CDec(currentStock) >= requiredQuantity
        Catch ex As Exception
            Throw New Exception($"Error verificando disponibilidad de stock: {ex.Message}", ex)
        End Try
    End Function
    
End Module

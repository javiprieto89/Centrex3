Imports System.Data.SqlClient

Module pedidos
    ' ************************************ FUNCIONES DE PEDIDOS REFACTORIZADAS ***************************
    
    ''' <summary>
    ''' Función refactorizada para obtener información de pedido usando DatabaseHelper
    ''' </summary>
    Public Function InfoPedido(Optional ByVal id_pedido As String = "") As pedido
        Try
            Dim sql As String
            Dim parameters As New Dictionary(Of String, Object)
            
            If String.IsNullOrEmpty(id_pedido) Then
                sql = "SET DATEFORMAT dmy; SELECT TOP 1 id_pedido, CONVERT(NVARCHAR(20), fecha, 3), CONVERT(NVARCHAR(20), fecha_edicion, 3), id_cliente, " &
                      "markup, subtotal, iva, total, nota1, nota2, esPresupuesto, activo, cerrado, ISNULL(idPresupuesto, 0), ISNULL(id_comprobante, 0), " &
                      "cae, CONVERT(NVARCHAR(20), fechaVencimiento_cae, 112), ISNULL(puntoVenta, 0), ISNULL(numeroComprobante, 0), " &
                      "ISNULL(codigoDeBarras, 0), ISNULL(esTest, 0), id_cc, ISNULL(numeroComprobante_anulado, 0), ISNULL(numeroPedido_anulado, 0), esDuplicado, id_usuario " &
                      "FROM pedidos ORDER by id_pedido DESC"
            Else
                sql = "SET DATEFORMAT dmy; SELECT id_pedido, CONVERT(NVARCHAR(20), fecha, 3), CONVERT(NVARCHAR(20), fecha_edicion, 3), id_cliente, " &
                      "markup, subtotal, iva, total, nota1, nota2, esPresupuesto, activo, cerrado, ISNULL(idPresupuesto, 0), ISNULL(id_comprobante, 0), " &
                      "cae, CONVERT(NVARCHAR(20), fechaVencimiento_cae, 112), ISNULL(puntoVenta, 0), ISNULL(numeroComprobante, 0), " &
                      "ISNULL(codigoDeBarras, 0), ISNULL(esTest, 0), id_cc, ISNULL(numeroComprobante_anulado, 0), ISNULL(numeroPedido_anulado, 0), esDuplicado, id_usuario " &
                      "FROM pedidos WHERE id_pedido = @id_pedido"
                parameters.Add("@id_pedido", id_pedido)
            End If
            
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim tmp As New pedido()
                
                tmp.id_pedido = row("id_pedido").ToString()
                tmp.fecha = row("fecha").ToString()
                tmp.fecha_edicion = row("fecha_edicion").ToString()
                tmp.id_cliente = row("id_cliente").ToString()
                tmp.markup = row("markup").ToString()
                tmp.subTotal = row("subtotal").ToString()
                tmp.iva = row("iva").ToString()
                tmp.total = row("total").ToString()
                tmp.nota1 = row("nota1").ToString()
                tmp.nota2 = row("nota2").ToString()
                tmp.esPresupuesto = CBool(row("esPresupuesto"))
                tmp.activo = CBool(row("activo"))
                tmp.cerrado = CBool(row("cerrado"))
                tmp.idPresupuesto = row("idPresupuesto").ToString()
                tmp.id_comprobante = row("id_comprobante").ToString()
                tmp.cae = row("cae").ToString()
                tmp.fechaVencimiento_cae = row("fechaVencimiento_cae").ToString()
                tmp.puntoVenta = row("puntoVenta").ToString()
                tmp.numeroComprobante = row("numeroComprobante").ToString()
                tmp.codigoDeBarras = row("codigoDeBarras").ToString()
                tmp.esTest = CBool(row("esTest"))
                tmp.id_Cc = row("id_cc").ToString()
                tmp.numeroComprobante_anulado = row("numeroComprobante_anulado").ToString()
                tmp.numeroPedido_anulado = row("numeroPedido_anulado").ToString()
                tmp.esDuplicado = CBool(row("esDuplicado"))
                tmp.id_usuario = row("id_usuario").ToString()
                
                Return tmp
            End If
            
            Return Nothing
        Catch ex As Exception
            ShowError($"Error obteniendo información de pedido: {ex.Message}", "Error")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para agregar pedido usando DatabaseHelper
    ''' </summary>
    Public Function AddPedido(p As pedido) As Boolean
        Try
            Dim sql As String = "INSERT INTO pedidos (fecha, fecha_edicion, id_cliente, markup, subtotal, iva, total, nota1, nota2, " &
                               "esPresupuesto, activo, cerrado, idPresupuesto, id_comprobante, cae, fechaVencimiento_cae, " &
                               "puntoVenta, numeroComprobante, codigoDeBarras, esTest, id_cc, numeroComprobante_anulado, " &
                               "numeroPedido_anulado, esDuplicado, id_usuario) " &
                               "VALUES (@fecha, @fecha_edicion, @id_cliente, @markup, @subtotal, @iva, @total, @nota1, @nota2, " &
                               "@esPresupuesto, @activo, @cerrado, @idPresupuesto, @id_comprobante, @cae, @fechaVencimiento_cae, " &
                               "@puntoVenta, @numeroComprobante, @codigoDeBarras, @esTest, @id_cc, @numeroComprobante_anulado, " &
                               "@numeroPedido_anulado, @esDuplicado, @id_usuario)"
            
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@fecha", p.fecha},
                {"@fecha_edicion", p.fecha_edicion},
                {"@id_cliente", p.id_cliente},
                {"@markup", p.markup},
                {"@subtotal", p.subTotal},
                {"@iva", p.iva},
                {"@total", p.total},
                {"@nota1", p.nota1},
                {"@nota2", p.nota2},
                {"@esPresupuesto", p.esPresupuesto},
                {"@activo", p.activo},
                {"@cerrado", p.cerrado},
                {"@idPresupuesto", p.idPresupuesto},
                {"@id_comprobante", p.id_comprobante},
                {"@cae", p.cae},
                {"@fechaVencimiento_cae", p.fechaVencimiento_cae},
                {"@puntoVenta", p.puntoVenta},
                {"@numeroComprobante", p.numeroComprobante},
                {"@codigoDeBarras", p.codigoDeBarras},
                {"@esTest", p.esTest},
                {"@id_cc", p.id_Cc},
                {"@numeroComprobante_anulado", p.numeroComprobante_anulado},
                {"@numeroPedido_anulado", p.numeroPedido_anulado},
                {"@esDuplicado", p.esDuplicado},
                {"@id_usuario", p.id_usuario}
            }
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch ex As Exception
            ShowError($"Error agregando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para actualizar pedido usando DatabaseHelper
    ''' </summary>
    Public Function UpdatePedido(p As pedido) As Boolean
        Try
            Dim sql As String = "UPDATE pedidos SET fecha = @fecha, fecha_edicion = @fecha_edicion, id_cliente = @id_cliente, " &
                               "markup = @markup, subtotal = @subtotal, iva = @iva, total = @total, nota1 = @nota1, nota2 = @nota2, " &
                               "esPresupuesto = @esPresupuesto, activo = @activo, cerrado = @cerrado, idPresupuesto = @idPresupuesto, " &
                               "id_comprobante = @id_comprobante, cae = @cae, fechaVencimiento_cae = @fechaVencimiento_cae, " &
                               "puntoVenta = @puntoVenta, numeroComprobante = @numeroComprobante, codigoDeBarras = @codigoDeBarras, " &
                               "esTest = @esTest, id_cc = @id_cc, numeroComprobante_anulado = @numeroComprobante_anulado, " &
                               "numeroPedido_anulado = @numeroPedido_anulado, esDuplicado = @esDuplicado, id_usuario = @id_usuario " &
                               "WHERE id_pedido = @id_pedido"
            
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id_pedido", p.id_pedido},
                {"@fecha", p.fecha},
                {"@fecha_edicion", p.fecha_edicion},
                {"@id_cliente", p.id_cliente},
                {"@markup", p.markup},
                {"@subtotal", p.subTotal},
                {"@iva", p.iva},
                {"@total", p.total},
                {"@nota1", p.nota1},
                {"@nota2", p.nota2},
                {"@esPresupuesto", p.esPresupuesto},
                {"@activo", p.activo},
                {"@cerrado", p.cerrado},
                {"@idPresupuesto", p.idPresupuesto},
                {"@id_comprobante", p.id_comprobante},
                {"@cae", p.cae},
                {"@fechaVencimiento_cae", p.fechaVencimiento_cae},
                {"@puntoVenta", p.puntoVenta},
                {"@numeroComprobante", p.numeroComprobante},
                {"@codigoDeBarras", p.codigoDeBarras},
                {"@esTest", p.esTest},
                {"@id_cc", p.id_Cc},
                {"@numeroComprobante_anulado", p.numeroComprobante_anulado},
                {"@numeroPedido_anulado", p.numeroPedido_anulado},
                {"@esDuplicado", p.esDuplicado},
                {"@id_usuario", p.id_usuario}
            }
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch ex As Exception
            ShowError($"Error actualizando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para eliminar pedido usando DatabaseHelper
    ''' </summary>
    Public Function BorrarPedido(pedidoId As String) As Boolean
        Try
            Return DeleteRecord("pedidos", pedidoId)
        Catch ex As Exception
            ShowError($"Error eliminando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para cerrar pedido usando DatabaseHelper
    ''' </summary>
    Public Function CerrarPedido(pedidoId As String, esPresupuesto As Boolean) As Boolean
        Try
            Dim sql As String = "UPDATE pedidos SET cerrado = 1 WHERE id_pedido = @id_pedido"
            Dim parameters As New Dictionary(Of String, Object) From {{"@id_pedido", pedidoId}}
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch ex As Exception
            ShowError($"Error cerrando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para duplicar pedido usando DatabaseHelper
    ''' </summary>
    Public Function DuplicarPedido(pedidoId As String) As Boolean
        Try
            Dim pedidoOriginal As pedido = InfoPedido(pedidoId)
            If pedidoOriginal Is Nothing Then
                ShowError("No se pudo obtener el pedido original", "Error")
                Return False
            End If
            
            ' Crear copia del pedido
            pedidoOriginal.id_pedido = ""
            pedidoOriginal.fecha = DateTime.Now.ToString("dd/MM/yyyy")
            pedidoOriginal.fecha_edicion = DateTime.Now.ToString("dd/MM/yyyy")
            pedidoOriginal.cerrado = False
            pedidoOriginal.esDuplicado = True
            
            Return AddPedido(pedidoOriginal)
        Catch ex As Exception
            ShowError($"Error duplicando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener información de item de pedido usando DatabaseHelper
    ''' </summary>
    Public Function InfoItemPedido(itemId As String) As item_pedido
        Try
            Dim sql As String = "SELECT * FROM pedidos_items WHERE id_item_pedido = @itemId"
            Dim parameters As New Dictionary(Of String, Object) From {{"@itemId", itemId}}
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                Dim item As New item_pedido()
                
                item.id_item_pedido = row("id_item_pedido").ToString()
                item.id_pedido = row("id_pedido").ToString()
                item.id_item = row("id_item").ToString()
                item.cantidad = CDec(row("cantidad"))
                item.precio = CDec(row("precio"))
                item.subtotal = CDec(row("subtotal"))
                item.activo = CBool(row("activo"))
                
                Return item
            End If
            
            Return Nothing
        Catch ex As Exception
            ShowError($"Error obteniendo información de item de pedido: {ex.Message}", "Error")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para agregar item a pedido usando DatabaseHelper
    ''' </summary>
    Public Function AddItemPedido(item As item_pedido) As Boolean
        Try
            Dim sql As String = "INSERT INTO pedidos_items (id_pedido, id_item, cantidad, precio, subtotal, activo) " &
                               "VALUES (@id_pedido, @id_item, @cantidad, @precio, @subtotal, @activo)"
            
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id_pedido", item.id_pedido},
                {"@id_item", item.id_item},
                {"@cantidad", item.cantidad},
                {"@precio", item.precio},
                {"@subtotal", item.subtotal},
                {"@activo", item.activo}
            }
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch ex As Exception
            ShowError($"Error agregando item a pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para actualizar item de pedido usando DatabaseHelper
    ''' </summary>
    Public Function UpdateItemPedido(item As item_pedido) As Boolean
        Try
            Dim sql As String = "UPDATE pedidos_items SET id_pedido = @id_pedido, id_item = @id_item, " &
                               "cantidad = @cantidad, precio = @precio, subtotal = @subtotal, activo = @activo " &
                               "WHERE id_item_pedido = @id_item_pedido"
            
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@id_item_pedido", item.id_item_pedido},
                {"@id_pedido", item.id_pedido},
                {"@id_item", item.id_item},
                {"@cantidad", item.cantidad},
                {"@precio", item.precio},
                {"@subtotal", item.subtotal},
                {"@activo", item.activo}
            }
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch ex As Exception
            ShowError($"Error actualizando item de pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para eliminar item de pedido usando DatabaseHelper
    ''' </summary>
    Public Function BorrarItemPedido(itemId As String) As Boolean
        Try
            Return DeleteRecord("pedidos_items", itemId)
        Catch ex As Exception
            ShowError($"Error eliminando item de pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para calcular impuestos de item usando BusinessLogicHelper
    ''' </summary>
    Public Function CalcularImpuestosItem(precio As Decimal, cantidad As Decimal, tasaIVA As Decimal) As Decimal
        Try
            Dim subtotal As Decimal = precio * cantidad
            Return CalculateIVA(subtotal, tasaIVA)
        Catch ex As Exception
            ShowError($"Error calculando impuestos: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para recalcular precios usando BusinessLogicHelper
    ''' </summary>
    Public Sub RecalcularPrecios(pedidoId As String)
        Try
            Dim sql As String = "SELECT * FROM pedidos_items WHERE id_pedido = @pedidoId AND activo = 1"
            Dim parameters As New Dictionary(Of String, Object) From {{"@pedidoId", pedidoId}}
            Dim dt As DataTable = ExecuteQuery(sql, parameters)
            
            Dim subtotalTotal As Decimal = 0
            Dim ivaTotal As Decimal = 0
            
            For Each row As DataRow In dt.Rows
                Dim cantidad As Decimal = CDec(row("cantidad"))
                Dim precio As Decimal = CDec(row("precio"))
                Dim subtotal As Decimal = cantidad * precio
                Dim iva As Decimal = CalculateIVA(subtotal, 21) ' Asumiendo 21% de IVA
                
                subtotalTotal += subtotal
                ivaTotal += iva
                
                ' Actualizar subtotal en la base de datos
                Dim updateSql As String = "UPDATE pedidos_items SET subtotal = @subtotal WHERE id_item_pedido = @id_item_pedido"
                Dim updateParams As New Dictionary(Of String, Object) From {
                    {"@subtotal", subtotal},
                    {"@id_item_pedido", row("id_item_pedido")}
                }
                ExecuteNonQuery(updateSql, updateParams)
            Next
            
            ' Actualizar totales del pedido
            Dim total As Decimal = CalculateTotal(subtotalTotal, ivaTotal)
            Dim updatePedidoSql As String = "UPDATE pedidos SET subtotal = @subtotal, iva = @iva, total = @total WHERE id_pedido = @id_pedido"
            Dim updatePedidoParams As New Dictionary(Of String, Object) From {
                {"@subtotal", subtotalTotal},
                {"@iva", ivaTotal},
                {"@total", total},
                {"@id_pedido", pedidoId}
            }
            ExecuteNonQuery(updatePedidoSql, updatePedidoParams)
            
        Catch ex As Exception
            ShowError($"Error recalculando precios: {ex.Message}", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Función refactorizada para obtener precio original usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPrecioOriginal(itemId As String) As Decimal
        Try
            Dim sql As String = "SELECT precio FROM items WHERE id_item = @itemId"
            Dim parameters As New Dictionary(Of String, Object) From {{"@itemId", itemId}}
            Dim result As Object = ExecuteScalar(sql, parameters)
            
            If result IsNot Nothing Then
                Return CDec(result)
            End If
            
            Return 0
        Catch ex As Exception
            ShowError($"Error obteniendo precio original: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener ID de item markup usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerIdItemMarkupPedido(pedidoId As String, itemId As String) As String
        Try
            Dim sql As String = "SELECT id_item_pedido FROM pedidos_items WHERE id_pedido = @pedidoId AND id_item = @itemId"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@pedidoId", pedidoId},
                {"@itemId", itemId}
            }
            Dim result As Object = ExecuteScalar(sql, parameters)
            
            If result IsNot Nothing Then
                Return result.ToString()
            End If
            
            Return ""
        Catch ex As Exception
            ShowError($"Error obteniendo ID de item markup: {ex.Message}", "Error")
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para verificar existencia de descuento markup usando DatabaseHelper
    ''' </summary>
    Public Function ExisteDescuentoMarkupTmp(pedidoId As String, itemId As String) As Boolean
        Try
            Dim sql As String = "SELECT COUNT(*) FROM tmppedidos_items WHERE id_pedido = @pedidoId AND id_item = @itemId"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@pedidoId", pedidoId},
                {"@itemId", itemId}
            }
            Dim count As Integer = CInt(ExecuteScalar(sql, parameters))
            
            Return count > 0
        Catch ex As Exception
            ShowError($"Error verificando descuento markup: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para preguntar cantidad cargada usando FormHelper
    ''' </summary>
    Public Function PreguntarCantidadCargada() As Decimal
        Try
            Dim cantidad As String = InputBox("Ingrese la cantidad:", "Cantidad", "1")
            If Decimal.TryParse(cantidad, cantidad) Then
                Return cantidad
            End If
            Return 0
        Catch ex As Exception
            ShowError($"Error preguntando cantidad: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para preguntar precio cargado usando FormHelper
    ''' </summary>
    Public Function PreguntarPrecioCargado() As Decimal
        Try
            Dim precio As String = InputBox("Ingrese el precio:", "Precio", "0")
            If Decimal.TryParse(precio, precio) Then
                Return precio
            End If
            Return 0
        Catch ex As Exception
            ShowError($"Error preguntando precio: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para convertir pedido a pedido temporal usando DatabaseHelper
    ''' </summary>
    Public Function ConvertirPedidoAPedidoTmp(pedidoId As String) As Boolean
        Try
            Dim pedido As pedido = InfoPedido(pedidoId)
            If pedido Is Nothing Then
                ShowError("No se pudo obtener el pedido", "Error")
                Return False
            End If
            
            ' Crear pedido temporal
            Dim sql As String = "INSERT INTO tmppedidos (fecha, fecha_edicion, id_cliente, markup, subtotal, iva, total, " &
                               "nota1, nota2, esPresupuesto, activo, cerrado, idPresupuesto, id_comprobante, cae, " &
                               "fechaVencimiento_cae, puntoVenta, numeroComprobante, codigoDeBarras, esTest, id_cc, " &
                               "numeroComprobante_anulado, numeroPedido_anulado, esDuplicado, id_usuario) " &
                               "VALUES (@fecha, @fecha_edicion, @id_cliente, @markup, @subtotal, @iva, @total, " &
                               "@nota1, @nota2, @esPresupuesto, @activo, @cerrado, @idPresupuesto, @id_comprobante, @cae, " &
                               "@fechaVencimiento_cae, @puntoVenta, @numeroComprobante, @codigoDeBarras, @esTest, @id_cc, " &
                               "@numeroComprobante_anulado, @numeroPedido_anulado, @esDuplicado, @id_usuario)"
            
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@fecha", pedido.fecha},
                {"@fecha_edicion", pedido.fecha_edicion},
                {"@id_cliente", pedido.id_cliente},
                {"@markup", pedido.markup},
                {"@subtotal", pedido.subTotal},
                {"@iva", pedido.iva},
                {"@total", pedido.total},
                {"@nota1", pedido.nota1},
                {"@nota2", pedido.nota2},
                {"@esPresupuesto", pedido.esPresupuesto},
                {"@activo", pedido.activo},
                {"@cerrado", pedido.cerrado},
                {"@idPresupuesto", pedido.idPresupuesto},
                {"@id_comprobante", pedido.id_comprobante},
                {"@cae", pedido.cae},
                {"@fechaVencimiento_cae", pedido.fechaVencimiento_cae},
                {"@puntoVenta", pedido.puntoVenta},
                {"@numeroComprobante", pedido.numeroComprobante},
                {"@codigoDeBarras", pedido.codigoDeBarras},
                {"@esTest", pedido.esTest},
                {"@id_cc", pedido.id_Cc},
                {"@numeroComprobante_anulado", pedido.numeroComprobante_anulado},
                {"@numeroPedido_anulado", pedido.numeroPedido_anulado},
                {"@esDuplicado", pedido.esDuplicado},
                {"@id_usuario", pedido.id_usuario}
            }
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch ex As Exception
            ShowError($"Error convirtiendo pedido a temporal: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener precio original de pedido usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPrecioOriginalPedido(pedidoId As String) As Decimal
        Try
            Dim sql As String = "SELECT SUM(subtotal) FROM pedidos_items WHERE id_pedido = @pedidoId AND activo = 1"
            Dim parameters As New Dictionary(Of String, Object) From {{"@pedidoId", pedidoId}}
            Dim result As Object = ExecuteScalar(sql, parameters)
            
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return CDec(result)
            End If
            
            Return 0
        Catch ex As Exception
            ShowError($"Error obteniendo precio original de pedido: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para guardar pedido usando DatabaseHelper
    ''' </summary>
    Public Function GuardarPedido(p As pedido) As Boolean
        Try
            If String.IsNullOrEmpty(p.id_pedido) Then
                Return AddPedido(p)
            Else
                Return UpdatePedido(p)
            End If
        Catch ex As Exception
            ShowError($"Error guardando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para validar pedido usando ValidationHelper
    ''' </summary>
    Public Function ValidarPedido(p As pedido) As Boolean
        Try
            If String.IsNullOrEmpty(p.id_cliente) Then
                ShowError("Debe seleccionar un cliente", "Validación")
                Return False
            End If
            
            If String.IsNullOrEmpty(p.id_comprobante) Then
                ShowError("Debe seleccionar un comprobante", "Validación")
                Return False
            End If
            
            If CDec(p.total) <= 0 Then
                ShowError("El total debe ser mayor a cero", "Validación")
                Return False
            End If
            
            Return True
        Catch ex As Exception
            ShowError($"Error validando pedido: {ex.Message}", "Error")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para formatear pedido usando BusinessLogicHelper
    ''' </summary>
    Public Function FormatearPedido(p As pedido) As String
        Try
            Dim formato As String = $"Pedido #{p.id_pedido} - Cliente: {p.id_cliente} - Total: {FormatPrice(CDec(p.total))}"
            Return formato
        Catch ex As Exception
            ShowError($"Error formateando pedido: {ex.Message}", "Error")
            Return "Error al formatear pedido"
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener estadísticas de pedidos usando MainDatabaseOperations
    ''' </summary>
    Public Function ObtenerEstadisticasPedidos() As Dictionary(Of String, Integer)
        Try
            Dim stats As New Dictionary(Of String, Integer)()
            
            stats.Add("TotalPedidos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos")))
            stats.Add("PedidosActivos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE activo = 1")))
            stats.Add("PedidosCerrados", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE cerrado = 1")))
            stats.Add("Presupuestos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE esPresupuesto = 1")))
            stats.Add("PedidosTest", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE esTest = 1")))
            
            Return stats
        Catch ex As Exception
            ShowError($"Error obteniendo estadísticas de pedidos: {ex.Message}", "Error")
            Return New Dictionary(Of String, Integer)()
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para exportar pedidos a CSV usando DataGridOperations
    ''' </summary>
    Public Sub ExportarPedidosACSV(fileName As String, Optional filters As String = "")
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, c.nombre as cliente, p.total, p.activo, p.cerrado " &
                               "FROM pedidos p INNER JOIN clientes c ON p.id_cliente = c.id_cliente"
            
            If Not String.IsNullOrEmpty(filters) Then
                sql += $" WHERE {filters}"
            End If
            
            sql += " ORDER BY p.id_pedido DESC"
            
            Dim dt As DataTable = ExecuteQuery(sql)
            
            ' Crear DataGridView temporal para exportar
            Dim tempGrid As New DataGridView()
            tempGrid.DataSource = dt
            
            ExportDataGridToCSV(tempGrid, fileName)
        Catch ex As Exception
            ShowError($"Error exportando pedidos a CSV: {ex.Message}", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Función refactorizada para exportar pedidos a Excel usando DataGridOperations
    ''' </summary>
    Public Sub ExportarPedidosAExcel(fileName As String, Optional filters As String = "")
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, c.nombre as cliente, p.total, p.activo, p.cerrado " &
                               "FROM pedidos p INNER JOIN clientes c ON p.id_cliente = c.id_cliente"
            
            If Not String.IsNullOrEmpty(filters) Then
                sql += $" WHERE {filters}"
            End If
            
            sql += " ORDER BY p.id_pedido DESC"
            
            Dim dt As DataTable = ExecuteQuery(sql)
            
            ' Crear DataGridView temporal para exportar
            Dim tempGrid As New DataGridView()
            tempGrid.DataSource = dt
            
            ExportDataGridToExcel(tempGrid, fileName)
        Catch ex As Exception
            ShowError($"Error exportando pedidos a Excel: {ex.Message}", "Error")
        End Try
    End Sub

    ''' <summary>
    ''' Función refactorizada para buscar pedidos usando DatabaseHelper
    ''' </summary>
    Public Function BuscarPedidos(searchText As String) As DataTable
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, c.nombre as cliente, p.total, p.activo, p.cerrado " &
                               "FROM pedidos p INNER JOIN clientes c ON p.id_cliente = c.id_cliente " &
                               "WHERE p.id_pedido LIKE @search OR c.nombre LIKE @search OR p.total LIKE @search " &
                               "ORDER BY p.id_pedido DESC"
            
            Dim parameters As New Dictionary(Of String, Object) From {{"@search", $"%{searchText}%"}}
            
            Return ExecuteQuery(sql, parameters)
        Catch ex As Exception
            ShowError($"Error buscando pedidos: {ex.Message}", "Error")
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener pedidos por cliente usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPedidosPorCliente(clienteId As String) As DataTable
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, p.total, p.activo, p.cerrado " &
                               "FROM pedidos p WHERE p.id_cliente = @clienteId ORDER BY p.id_pedido DESC"
            
            Dim parameters As New Dictionary(Of String, Object) From {{"@clienteId", clienteId}}
            
            Return ExecuteQuery(sql, parameters)
        Catch ex As Exception
            ShowError($"Error obteniendo pedidos por cliente: {ex.Message}", "Error")
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener pedidos por fecha usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPedidosPorFecha(fechaInicio As DateTime, fechaFin As DateTime) As DataTable
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, c.nombre as cliente, p.total, p.activo, p.cerrado " &
                               "FROM pedidos p INNER JOIN clientes c ON p.id_cliente = c.id_cliente " &
                               "WHERE p.fecha BETWEEN @fechaInicio AND @fechaFin ORDER BY p.id_pedido DESC"
            
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@fechaInicio", fechaInicio},
                {"@fechaFin", fechaFin}
            }
            
            Return ExecuteQuery(sql, parameters)
        Catch ex As Exception
            ShowError($"Error obteniendo pedidos por fecha: {ex.Message}", "Error")
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener total de ventas usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerTotalVentas(Optional fechaInicio As DateTime? = Nothing, Optional fechaFin As DateTime? = Nothing) As Decimal
        Try
            Dim sql As String = "SELECT SUM(total) FROM pedidos WHERE activo = 1 AND cerrado = 1"
            Dim parameters As New Dictionary(Of String, Object)()
            
            If fechaInicio.HasValue And fechaFin.HasValue Then
                sql += " AND fecha BETWEEN @fechaInicio AND @fechaFin"
                parameters.Add("@fechaInicio", fechaInicio.Value)
                parameters.Add("@fechaFin", fechaFin.Value)
            End If
            
            Dim result As Object = ExecuteScalar(sql, parameters)
            
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return CDec(result)
            End If
            
            Return 0
        Catch ex As Exception
            ShowError($"Error obteniendo total de ventas: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener cantidad de pedidos usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerCantidadPedidos(Optional fechaInicio As DateTime? = Nothing, Optional fechaFin As DateTime? = Nothing) As Integer
        Try
            Dim sql As String = "SELECT COUNT(*) FROM pedidos WHERE activo = 1"
            Dim parameters As New Dictionary(Of String, Object)()
            
            If fechaInicio.HasValue And fechaFin.HasValue Then
                sql += " AND fecha BETWEEN @fechaInicio AND @fechaFin"
                parameters.Add("@fechaInicio", fechaInicio.Value)
                parameters.Add("@fechaFin", fechaFin.Value)
            End If
            
            Return CInt(ExecuteScalar(sql, parameters))
        Catch ex As Exception
            ShowError($"Error obteniendo cantidad de pedidos: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener promedio de ventas usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPromedioVentas(Optional fechaInicio As DateTime? = Nothing, Optional fechaFin As DateTime? = Nothing) As Decimal
        Try
            Dim sql As String = "SELECT AVG(total) FROM pedidos WHERE activo = 1 AND cerrado = 1"
            Dim parameters As New Dictionary(Of String, Object)()
            
            If fechaInicio.HasValue And fechaFin.HasValue Then
                sql += " AND fecha BETWEEN @fechaInicio AND @fechaFin"
                parameters.Add("@fechaInicio", fechaInicio.Value)
                parameters.Add("@fechaFin", fechaFin.Value)
            End If
            
            Dim result As Object = ExecuteScalar(sql, parameters)
            
            If result IsNot Nothing AndAlso Not IsDBNull(result) Then
                Return CDec(result)
            End If
            
            Return 0
        Catch ex As Exception
            ShowError($"Error obteniendo promedio de ventas: {ex.Message}", "Error")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener pedidos pendientes usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPedidosPendientes() As DataTable
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, c.nombre as cliente, p.total " &
                               "FROM pedidos p INNER JOIN clientes c ON p.id_cliente = c.id_cliente " &
                               "WHERE p.activo = 1 AND p.cerrado = 0 ORDER BY p.id_pedido DESC"
            
            Return ExecuteQuery(sql)
        Catch ex As Exception
            ShowError($"Error obteniendo pedidos pendientes: {ex.Message}", "Error")
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener pedidos vencidos usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerPedidosVencidos() As DataTable
        Try
            Dim sql As String = "SELECT p.id_pedido, p.fecha, c.nombre as cliente, p.total, p.fechaVencimiento_cae " &
                               "FROM pedidos p INNER JOIN clientes c ON p.id_cliente = c.id_cliente " &
                               "WHERE p.activo = 1 AND p.fechaVencimiento_cae < GETDATE() ORDER BY p.id_pedido DESC"
            
            Return ExecuteQuery(sql)
        Catch ex As Exception
            ShowError($"Error obteniendo pedidos vencidos: {ex.Message}", "Error")
            Return New DataTable()
        End Try
    End Function

    ''' <summary>
    ''' Función refactorizada para obtener resumen de pedidos usando DatabaseHelper
    ''' </summary>
    Public Function ObtenerResumenPedidos() As Dictionary(Of String, Object)
        Try
            Dim resumen As New Dictionary(Of String, Object)()
            
            resumen.Add("TotalPedidos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE activo = 1")))
            resumen.Add("PedidosCerrados", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE activo = 1 AND cerrado = 1")))
            resumen.Add("PedidosPendientes", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE activo = 1 AND cerrado = 0")))
            resumen.Add("TotalVentas", CDec(ExecuteScalar("SELECT SUM(total) FROM pedidos WHERE activo = 1 AND cerrado = 1")))
            resumen.Add("PromedioVentas", CDec(ExecuteScalar("SELECT AVG(total) FROM pedidos WHERE activo = 1 AND cerrado = 1")))
            resumen.Add("Presupuestos", CInt(ExecuteScalar("SELECT COUNT(*) FROM pedidos WHERE activo = 1 AND esPresupuesto = 1")))
            
            Return resumen
        Catch ex As Exception
            ShowError($"Error obteniendo resumen de pedidos: {ex.Message}", "Error")
            Return New Dictionary(Of String, Object)()
        End Try
    End Function

End Module
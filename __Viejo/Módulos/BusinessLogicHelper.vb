Imports System.Collections.Generic

''' <summary>
''' Módulo helper para lógica de negocio común
''' Centraliza cálculos y reglas de negocio repetitivas
''' </summary>
Module BusinessLogicHelper
    
    ''' <summary>
    ''' Calcula el subtotal de un pedido
    ''' </summary>
    Public Function CalculateSubtotal(items As List(Of item_pedido)) As Decimal
        Dim subtotal As Decimal = 0
        For Each item In items
            subtotal += item.cantidad * item.precio
        Next
        Return subtotal
    End Function
    
    ''' <summary>
    ''' Calcula el IVA sobre un subtotal
    ''' </summary>
    Public Function CalculateIVA(subtotal As Decimal, ivaPercentage As Decimal) As Decimal
        Return subtotal * (ivaPercentage / 100)
    End Function
    
    ''' <summary>
    ''' Calcula el total de un pedido
    ''' </summary>
    Public Function CalculateTotal(subtotal As Decimal, iva As Decimal, Optional descuento As Decimal = 0) As Decimal
        Return subtotal + iva - descuento
    End Function
    
    ''' <summary>
    ''' Calcula el markup sobre un precio de costo
    ''' </summary>
    Public Function CalculateMarkup(costo As Decimal, markupPercentage As Decimal) As Decimal
        Return costo * (1 + markupPercentage / 100)
    End Function
    
    ''' <summary>
    ''' Calcula el descuento sobre un precio
    ''' </summary>
    Public Function CalculateDiscount(precio As Decimal, discountPercentage As Decimal) As Decimal
        Return precio * (discountPercentage / 100)
    End Function
    
    ''' <summary>
    ''' Calcula el precio final con descuento aplicado
    ''' </summary>
    Public Function CalculateFinalPrice(precio As Decimal, discountPercentage As Decimal) As Decimal
        Dim descuento As Decimal = CalculateDiscount(precio, discountPercentage)
        Return precio - descuento
    End Function
    
    ''' <summary>
    ''' Valida si un cliente tiene crédito disponible
    ''' </summary>
    Public Function HasAvailableCredit(clienteId As Integer, monto As Decimal) As Boolean
        Try
            Dim sql As String = "SELECT limite_credito FROM clientes WHERE id_cliente = @clienteId"
            Dim parameters As New Dictionary(Of String, Object) From {{"@clienteId", clienteId}}
            Dim limiteCredito As Object = ExecuteScalar(sql, parameters)
            
            If limiteCredito Is Nothing Then Return False
            
            Dim limite As Decimal = CDec(limiteCredito)
            Dim saldoActual As Decimal = GetClientBalance(clienteId)
            
            Return (saldoActual + monto) <= limite
        Catch
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Obtiene el saldo actual de un cliente
    ''' </summary>
    Public Function GetClientBalance(clienteId As Integer) As Decimal
        Try
            Dim sql As String = "SELECT ISNULL(SUM(debe - haber), 0) FROM cc_clientes WHERE id_cliente = @clienteId"
            Dim parameters As New Dictionary(Of String, Object) From {{"@clienteId", clienteId}}
            Dim saldo As Object = ExecuteScalar(sql, parameters)
            
            Return If(saldo Is Nothing, 0, CDec(saldo))
        Catch
            Return 0
        End Try
    End Function
    
    ''' <summary>
    ''' Valida si hay stock suficiente para un item
    ''' </summary>
    Public Function HasEnoughStock(itemId As Integer, cantidad As Decimal) As Boolean
        Try
            Dim sql As String = "SELECT stock_actual FROM items WHERE id_item = @itemId"
            Dim parameters As New Dictionary(Of String, Object) From {{"@itemId", itemId}}
            Dim stockActual As Object = ExecuteScalar(sql, parameters)
            
            If stockActual Is Nothing Then Return False
            
            Return CDec(stockActual) >= cantidad
        Catch
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Actualiza el stock de un item
    ''' </summary>
    Public Function UpdateItemStock(itemId As Integer, cantidad As Decimal, tipoMovimiento As String) As Boolean
        Try
            Dim factor As Integer = If(tipoMovimiento = "ENTRADA", 1, -1)
            Dim sql As String = "UPDATE items SET stock_actual = stock_actual + (@cantidad * @factor) WHERE id_item = @itemId"
            Dim parameters As New Dictionary(Of String, Object) From {
                {"@cantidad", cantidad},
                {"@factor", factor},
                {"@itemId", itemId}
            }
            
            ExecuteNonQuery(sql, parameters)
            Return True
        Catch
            Return False
        End Try
    End Function
    
    ''' <summary>
    ''' Genera el próximo número de pedido
    ''' </summary>
    Public Function GetNextPedidoNumber() As Integer
        Try
            Dim sql As String = "SELECT ISNULL(MAX(id_pedido), 0) + 1 FROM pedidos"
            Return CInt(ExecuteScalar(sql))
        Catch
            Return 1
        End Try
    End Function
    
    ''' <summary>
    ''' Genera el próximo número de comprobante
    ''' </summary>
    Public Function GetNextComprobanteNumber(tipoComprobante As Integer) As Integer
        Try
            Dim sql As String = "SELECT ISNULL(MAX(numeroComprobante), 0) + 1 FROM pedidos WHERE id_tipoComprobante = @tipo"
            Dim parameters As New Dictionary(Of String, Object) From {{"@tipo", tipoComprobante}}
            Return CInt(ExecuteScalar(sql, parameters))
        Catch
            Return 1
        End Try
    End Function
    
    ''' <summary>
    ''' Calcula la edad de un cliente basada en su fecha de nacimiento
    ''' </summary>
    Public Function CalculateAge(fechaNacimiento As Date) As Integer
        Dim today As Date = DateTime.Today
        Dim age As Integer = today.Year - fechaNacimiento.Year
        
        If fechaNacimiento.Date > today.AddYears(-age) Then
            age -= 1
        End If
        
        Return age
    End Function
    
    ''' <summary>
    ''' Valida si una fecha está en el rango de trabajo
    ''' </summary>
    Public Function IsWorkingDate(fecha As Date) As Boolean
        ' Lunes a Viernes, excluyendo feriados
        If fecha.DayOfWeek = DayOfWeek.Saturday OrElse fecha.DayOfWeek = DayOfWeek.Sunday Then
            Return False
        End If
        
        ' Aquí se podrían agregar validaciones de feriados
        Return True
    End Function
    
    ''' <summary>
    ''' Formatea un número de teléfono
    ''' </summary>
    Public Function FormatPhoneNumber(phone As String) As String
        If String.IsNullOrEmpty(phone) Then Return ""
        
        ' Remover caracteres no numéricos
        Dim cleanPhone As String = System.Text.RegularExpressions.Regex.Replace(phone, "[^\d]", "")
        
        ' Formatear según la longitud
        Select Case cleanPhone.Length
            Case 10
                Return $"{cleanPhone.Substring(0, 3)}-{cleanPhone.Substring(3, 3)}-{cleanPhone.Substring(6, 4)}"
            Case 11
                Return $"{cleanPhone.Substring(0, 1)}-{cleanPhone.Substring(1, 3)}-{cleanPhone.Substring(4, 3)}-{cleanPhone.Substring(7, 4)}"
            Case Else
                Return phone ' Devolver original si no coincide con formato esperado
        End Select
    End Function
    
    ''' <summary>
    ''' Valida si un precio está dentro del rango aceptable
    ''' </summary>
    Public Function IsPriceInRange(precio As Decimal, precioMinimo As Decimal, precioMaximo As Decimal) As Boolean
        Return precio >= precioMinimo AndAlso precio <= precioMaximo
    End Function
    
End Module

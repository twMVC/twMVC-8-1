Imports System.Net.Http
Imports System.Net.Http.Headers

Module Module1

    ''' <summary>
    ''' 取得指定uri裡的Source資源
    ''' </summary>
    ''' <param name="uri">Uri</param>
    ''' <param name="source">資源</param>
    Private Function GetResource(uri As String, source As String) As HttpResponseMessage
        Dim client As New HttpClient()
        Dim response As HttpResponseMessage = Nothing
        ' 資源在這裡
        client.BaseAddress = New Uri(uri)
        ' 指定格式
        ' client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/xml"))
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        Console.WriteLine(source)
        Console.ReadLine()
        response = client.GetAsync(source).Result()

        Return response
    End Function

    Private Sub ShowProducts(response As HttpResponseMessage)
        ' 狀態碼 200，才執行
        If response.IsSuccessStatusCode Then
            Dim products = response.Content.ReadAsAsync(Of IEnumerable(Of Product))().Result()

            Console.Clear()
            For Each p In products
                Console.WriteLine("Id:{0}, Name:{1}, Price: {2}", p.ProductID, p.ProductName, p.UnitPrice)
            Next
            Console.ReadLine()
        Else
            ShowErrorMessage(response)
        End If
    End Sub

    ''' <summary>
    ''' 成功，顯示產品資料
    ''' 失敗，顯示錯誤訊息
    ''' </summary>
    Private Sub ShowProduct(response As HttpResponseMessage)
        If response.IsSuccessStatusCode Then
            Dim product = response.Content.ReadAsAsync(Of Product).Result()
            Console.Clear()
            Console.WriteLine("Id:{0}, Name:{1}, Price: {2}", product.ProductID, product.ProductName, product.UnitPrice)
            Console.ReadLine()
        Else
            ShowErrorMessage(response)
        End If
    End Sub

    Private Async Sub ShowBankInfomation()
        Dim uri As String = "http://api.worldbank.org/countries?format=json"
        Await ShowNetworkAPI(uri)
    End Sub

    Private Async Sub ShowTwitter()
        Dim uri As String = "http://search.twitter.com/search.json?q=kkbruce&rpp=5&include_entities=true&result_type=mixed"
        Await ShowNetworkAPI(uri)
    End Sub

    Private Async Sub showGoogleGeocode()
        Dim uri As String = "http://maps.googleapis.com/maps/api/geocode/json?address=台北市羅斯福路4段85號&sensor=false"
        Await ShowNetworkAPI(uri)
    End Sub

    ''' <summary>
    ''' 使用非同步的存取
    ''' </summary>
    ''' <param name="uri">要存取的uri資源</param>
    Private Async Function ShowNetworkAPI(uri As String) As Task
        Dim client As New HttpClient()
        Dim response As HttpResponseMessage = Await client.GetAsync(uri)
        response.EnsureSuccessStatusCode()

        Dim content As String = Await response.Content.ReadAsStringAsync()
        Console.Clear()
        Console.WriteLine(content)
        Console.ReadLine()
    End Function

    Private Sub ShowErrorMessage(ByVal response As HttpResponseMessage)
        Console.WriteLine("{0}  ({1})", CType(response.StatusCode, Integer), response.ReasonPhrase)
    End Sub

    Sub Main()

        ' 全部產品
        Dim Products As HttpResponseMessage = GetResource("http://localhost:9527/", "api/Products")
        ShowProducts(Products)

        ' 某一id產品
        Console.WriteLine("請輸入要查詢的產品編號(id)：")
        Dim id As String = Console.ReadLine()
        ' 注意，Fiddler要開啟，不然會產生意外
        Dim ProductById As HttpResponseMessage = GetResource("http://localhost.fiddler:9527/", "api/Products" & "/" & id)
        ShowProduct(ProductById)
        '
        '' 取得外部Web API
        'ShowContries()
        'ShowTwitter()
        'showGoogleGeocode()

        Console.ReadLine()
        Console.WriteLine("bye bye.")
    End Sub

End Module

## TWMVC#8 ASP.NET WEB API DEMO PROJECT 說明檔 ##

**上半場範例**

以下為依投影片進行序順與說明：

1. twMVC.WebAPI
	1. 此為空白專案，主要示範：
	1. 建立空白含CRUD的ValuesController
	1. 建立Northwind資料庫
	1. 加入EDMX
	1. 處理物件循環參考
	1. 請參考：[http://blog.kkbruce.net/2013/03/aspnet-web-api-entity-framework-edmx-Navigation-Property-json-Self-referencing-loop-error.html](http://blog.kkbruce.net/2013/03/aspnet-web-api-entity-framework-edmx-Navigation-Property-json-Self-referencing-loop-error.html "點擊觀看")
1. twMVC.Northwind
	1. 此為含Northwind資料，含CRUD以及Patch的程式碼。
	1. 可透過PostMan與Fiddler去測試。
	1. PostmanController的程式碼，我會公佈在Blog上。
1. twMVC.HttpClientConsole
	1. 透過HttpClient類別去存取ASP.NET Web API與其他Web API服務。
1. twMVC.AzureBlog
	1.請不要直接抄去用，我不負責。
	1. 請參考小朱的大作：[http://books.gotop.com.tw/v_ACL036700](http://books.gotop.com.tw/v_ACL036700)，用力學習。
1. twMVC.Northwind
	1. 將ProductsControler進行以下修改，以提供OData：

    <Queryable>
    Function GetProducts() As IQueryable(Of Product)
    Return db.Products
    End Function


	2. Hpelp Page請Area目錄之下
1. twMVC.ProductService
	1.OData Service … 不說明。

1. 其他
	1.Poster目錄：ASP.NET Web API海報兩張。（由 http://www.asp.net/web-api 下載取得。）
	2. 3617.WebApiHelpPageGenerator.zip 離線產生Help Page執行檔。（版權為原作者所有。http://blogs.msdn.com/b/yaohuang1/archive/2013/01/20/design-time-generation-of-help-page-or-proxy-for-asp-net-web-api.aspx)
	3. Chapter APPX - 關於 MCSD Web Application 認證考試.docx（感謝小朱提供與授權，《ASP.NET MVC 4網站開發美學》二刷會直接附上。）
	4. ASP.NET_Web_API.pdf：此為本場次投影片PDF檔案。（版權為 Bruce 所有。）


----------------------------
此為研討會範例檔案


為了避免檔案過大，因此使用的套件皆無上傳，請自行還原，還原方式請參考
[http://demo.tc/Post/763](http://demo.tc/Post/763 "NuGet套件還原步驟使用Visual Studio 2012 為例")

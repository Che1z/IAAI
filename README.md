
<h1 align="center" style="font-weight: 700"><img alt="MVC5" src="https://img.shields.io/badge/.Net_Framework-MVC_5-Green"> 消防協會組織網站  | .Net Framework MVC 5 </h1>
<div align="center" style="margin-bottom:24px">

 <p align="center">
    <img width="1200" src ="https://github.com/Che1z/IAAI/blob/master/IAAI_View.png">
</p>
 

     
此網站為個人全端實作 MVC 5 網站，包含網站前台、管理者後台等兩區域
前台使用者除可以進行網站瀏覽，亦可進行登入、註冊及發文、留言
後臺管理者可以使用 CK Editor 進行訊息撰寫、亦可針對特定頁面資料進行 CRUD 存取
</div>

<h2 align="center" >功能介紹</h2>

> 身份分為「網站瀏覽者」、「前台會員」及「後台管理者」角色

### ► 網站瀏覽者

- 瀏覽網站頁面
  
- 前台會員註冊與登入
  
- 信件通知網站管理者


### ► 前台會員

- 具備「網站瀏覽者」之角色功能
  
- 啟用留言板功能，具備建立貼文、回覆文章功能


### ► 後台管理者 

- 編輯特定頁面之資訊

- CRUD特定頁面之資料

- 管理會員後台頁面存取權限


個人產出
後端開發環境：

框架：.NET Framework 4.7.2
專案：ASP.NET MVC 5
開發技術：
 <div align="center">
    <img alt="Visual_Studio" src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white" />
    <img alt=".NET" src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
    <img alt="C#" src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white" />
    <img alt="MVC5" src="https://img.shields.io/badge/MVC5-007ACC?style=for-the-badge&logoColor=white" />

  </div>
  <div align="center">
    <img alt="GIT" src="https://img.shields.io/badge/GIT-E44C30?style=for-the-badge&logo=git&logoColor=white" />
    <img alt="GitHUB" src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white" />
    <img alt="Entity_Framework" src="https://img.shields.io/badge/Entity_Framework-yellow?style=for-the-badge">
    <img alt="LINQ" src="https://img.shields.io/badge/LINQ-8A2BE2?style=for-the-badge">
    <img alt="SQL" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" />  
  </div>

區域路由：透過 Areas 建立後台網域，並將前台與後台會員權限分離

資料庫存取：Microsoft SQL Server 搭配 Entity Framework Code First 以及 LINQ 進行資料庫存取

權限控管：自定義 Filter 達成特定 Controller 的權限控管

遞迴函式：將 Navbar 與 Sidebar 透過Controller以及ActionName查詢方式自動生成

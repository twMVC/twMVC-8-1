﻿'------------------------------------------------------------------------------
' <auto-generated>
'    這個程式碼是由範本產生。
'
'    對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
'    如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Partial Public Class NorthwindEntities
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=NorthwindEntities")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        Throw New UnintentionalCodeFirstException()
    End Sub

    Public Property Products() As DbSet(Of Product)

End Class
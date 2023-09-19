# 測試記錄要項說明
## Logger.cs-TurnKeyFilesParse

若空白(未設定)就預設在專案的Logs下ConfigurationManager.AppSettings["LogPath"]

## Log及Exception處理流程-TurnKeyFilesParse

1.裡層程式丟出exception
  ``` c#
  throw new DirectoryNotFoundException(path);
  ```
2.取消Controller catch:透過DelegatingHandler寫log和打包訊息Controller catch並寫詳細log, 再丟出, 避免出現不適當的前端訊息

   catch (Exception ex)
   {
       Logger.Warn(ex.ToString());
       return this.BadRequest();
   }
   
3.DelegatingHandler(WrappingHandler.cs) catch response, 分析後發現非code 200, 將錯誤訊息用統一的api訊息打包後丟到前端

   if (response.TryGetContentValue(out content)
       && !response.IsSuccessStatusCode)
   {
	...
	#if DEBUG
						errorMessage = string.Concat(errorMessage, error.ExceptionMessage, error.StackTrace);
						Logger.Warn(errorMessage);
	#else
						Logger.Warn(error.ExceptionMessage);
	#endif
   }
   ...
   
   return request
    .CreateResponse(
        response.StatusCode,
        new ApiResponse(response.StatusCode, content, errorMessage));
	
4.以檢查路徑是否存在為例, 不存在就丟訊息 throw new DirectoryNotFoundException(path);

2023/08/28 10:42:23 //this is release.ExceptionMessage. 
	System.IO.DirectoryNotFoundException: D:\Doc\E0501
   於 Api.TurnKeyItemFactory.GetTurnKeyItem(TurnKeyItemEnum item) 於 D:\Dev\Asp.NetDemo\AspNetApi\TurnKeyItemFactory.cs: 行 26
   於 TurnKeyFilesAPI.TurnKeyController.<GetJson>d__0.MoveNext() 於 D:\Dev\Asp.NetDemo\AspNetApi\Controllers\TurnKeyController.cs: 行 36
	2023/08/28 10:42:23 //this is release.StackTrace.    
	於 TurnKeyFilesAPI.TurnKeyController.<GetJson>d__0.MoveNext() 於 D:\Dev\Asp.NetDemo\AspNetApi\Controllers\TurnKeyController.cs: 行 65
	--- 先前擲回例外狀況之位置中的堆疊追蹤結尾 ---
	   於 System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
	   於 System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
	   於 System.Threading.Tasks.TaskHelpersExtensions.<CastToObject>d__1`1.MoveNext()
	...

			'Bach Harvest File Path
			 Dim configSettings As AppServerConfigSettings = AppServerConfig.GetSettings(si)
			 Dim folderPath As String = FileShareFolderHelper.GetBatchFolderForApp(si,True,configSettings.FileShareRootFolder,si.AppToken.AppName) & "\" & "Harvest" & "\" & "Group.csv"
			 'Read the File
			 Dim FileReader As New System.IO.StreamReader(folderPath)
			 'Store the data
			 Dim stringReader As String = String.Empty
			 stringReader = FileReader.ReadLine()
        	'Read Each Line
			 For Each line As String In File.ReadLines(folderPath)
				'Brapi.ErrorLog.LogMessage(si,line)	
				'Assign the Security GroupName from File to the String
				Dim NewSecurityGroupName As String = line
				Dim objGroupInfo1 As GroupInfo = BRApi.Security.Admin.GetGroup(si, NewSecurityGroupName)
				'Check Group Already Exists
				If  objGroupInfo1 Is Nothing Then
				'Create a Object for Class Group
			 	Dim objGroup As Group = New Group()
				objGroup.Name = NewSecurityGroupName
					'Create a Object for Class GroupInfo
				Dim objGroupInfo As GroupInfo = New GroupInfo()
				objGroupInfo.Group = objGroup
					'Check the Group Already Exists
				BRApi.ErrorLog.LogMessage(si,NewSecurityGroupName &  " " & "Group Created")
						BRApi.Security.Admin.SaveGroup(si, objGroupInfo, False, Nothing, TriStateBool.TrueValue)
				Else
					BRApi.ErrorLog.LogMessage(si,NewSecurityGroupName &  " " & "Group Already Exists")
					End If
			Next
			FileReader.Close()
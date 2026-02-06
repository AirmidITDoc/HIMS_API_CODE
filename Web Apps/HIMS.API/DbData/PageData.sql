SELECT 
'IF NOT EXISTS(SELECT 1 FROM MenuMaster WHERE PermissionCode='''+PermissionCode+''')'+
'INSERT INTO MenuMaster(UpId,LinkName,Icon,LinkAction,SortOrder,IsActive,IsDisplay,PermissionCode,TableNames)'+
'VALUES((SELECT Id FROM MenuMaster WHERE LinkName='''+ISNULL((SELECT LinkName FROM MenuMaster WHERE Id=UpId),'''')+'),'''+LinkName+''','''+Icon+''','''+LinkAction+''',
'''+CONVERT(NVARCHAR,ISNULL(SortOrder,''))+''',1,1,'''+ISNULL(PermissionCode,'')+''','''+ISNULL(TableNames,'')+''')',

* FROM MenuMaster WHERE ISNULL(PermissionCode,'')<>''

-- for generate script for menu table



------------------- Gastrology----------------------------

IF NOT EXISTS(SELECT 1 FROM MenuMaster WHERE PermissionCode='GastrologyCasePaper')
	INSERT INTO MenuMaster(UpId,LinkName,Icon,LinkAction,SortOrder,isActive,IsDisplay,PermissionCode,TableNames)
	values(2,'Gastrology Case Paper','apps','#',99,1,0,'GastrologyCasePaper',NULL)
IF NOT EXISTS(SELECT 1 FROM MenuMaster WHERE PermissionCode='CheckIn')
	INSERT INTO MenuMaster(UpId,LinkName,Icon,LinkAction,SortOrder,isActive,IsDisplay,PermissionCode,TableNames)
	values(2,'Check In','apps','#',99,1,0,'CheckIn',NULL)
IF NOT EXISTS(SELECT 1 FROM MenuMaster WHERE PermissionCode='CheckOut')
	INSERT INTO MenuMaster(UpId,LinkName,Icon,LinkAction,SortOrder,isActive,IsDisplay,PermissionCode,TableNames)
	values(2,'Check Out','apps','#',99,1,0,'CheckOut',NULL)
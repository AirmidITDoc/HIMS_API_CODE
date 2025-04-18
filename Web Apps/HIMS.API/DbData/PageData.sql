SELECT 
'IF NOT EXISTS(SELECT 1 FROM MenuMaster WHERE PermissionCode='''+PermissionCode+''')'+
'INSERT INTO MenuMaster(UpId,LinkName,Icon,LinkAction,SortOrder,IsActive,IsDisplay,PermissionCode,TableNames)'+
'VALUES((SELECT Id FROM MenuMaster WHERE LinkName='''+ISNULL((SELECT LinkName FROM MenuMaster WHERE Id=UpId),'''')+'),'''+LinkName+''','''+Icon+''','''+LinkAction+''',
'''+CONVERT(NVARCHAR,ISNULL(SortOrder,''))+''',1,1,'''+ISNULL(PermissionCode,'')+''','''+ISNULL(TableNames,'')+''')',

* FROM MenuMaster WHERE ISNULL(PermissionCode,'')<>''

-- for generate script for menu table
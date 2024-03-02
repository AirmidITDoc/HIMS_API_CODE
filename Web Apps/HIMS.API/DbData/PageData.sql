IF NOT EXISTS(SELECT 1 FROM ModuleMaster WHERE ModuleName='Master')
	INSERT INTO ModuleMaster(ModuleName,IsActive) VALUES('Master',1)

IF NOT EXISTS(SELECT 1 FROM PageMaster WHERE PageCode='Prefix')
	INSERT INTO PageMaster(ModuleId,PageCode,PageName,IsActive,IsShowMenu,TableNames)
		VALUES((SELECT Id FROM ModuleMaster WHERE ModuleName='Master'),'Prefix','Prefix',1,1,'DB_PrefixMaster')

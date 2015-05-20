﻿CREATE TABLE [accounts] (
  [id_accounts] int IDENTITY (1,1) NOT NULL, 
	[id_currencies] nvarchar(3) NOT NULL, 
	[account_type] int NOT NULL, 
	[account_name] nvarchar(100) NOT NULL, 
	[account_details] ntext NOT NULL, 
	[icon] nvarchar(100) NOT NULL, 
	[total_income_entries] int DEFAULT (0) NOT NULL, 
	[total_expense_entries] int DEFAULT (0) NOT NULL, 
	[last_operation] datetime NULL, 
	[date_created] datetime DEFAULT (GETDATE()) NOT NULL
);
GO
ALTER TABLE [accounts] ADD CONSTRAINT [PK_accounts] PRIMARY KEY ([id_accounts]);
GO
CREATE TABLE [categories] (
  [id_categories] int IDENTITY (1,1) NOT NULL, 
	[parent_id] int DEFAULT (0) NOT NULL, 
	[category_name] nvarchar(100) NOT NULL, 
	[icon] nvarchar(100) NOT NULL, 
	[fore_color] int NOT NULL, 
	[back_color] int NOT NULL, 
	[total_entries] int NOT NULL, 
	[last_operation] datetime NULL, 
	[date_created] datetime DEFAULT (getdate()) NOT NULL
);
GO
ALTER TABLE [categories] ADD CONSTRAINT [PK_categories] PRIMARY KEY ([id_categories]);
GO
CREATE TABLE [currencies] (
  [id_currencies] nvarchar(3) NOT NULL, 
	[long_name] nvarchar(100) NOT NULL, 
	[short_name] nvarchar(10) NOT NULL, 
	[priority] int DEFAULT (0) NOT NULL, 
	[icon] nvarchar(100) NOT NULL
);
GO
ALTER TABLE [currencies] ADD CONSTRAINT [PK_currencies] PRIMARY KEY ([id_currencies]);
GO
CREATE TABLE [entries] (
  [id_entries] int IDENTITY (1,1) NOT NULL, 
	[id_categories] int NOT NULL, 
	[id_accounts] int NOT NULL, 
	[entry_type] tinyint NOT NULL, 
	[title] nvarchar(100) NOT NULL, 
	[description] ntext NOT NULL, 
	[amount] money NOT NULL, 
	[date_updated] datetime NULL, 
	[date_created] datetime NOT NULL
);
GO
ALTER TABLE [entries] ADD CONSTRAINT [PK_entries] PRIMARY KEY ([id_entries]);
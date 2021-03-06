﻿CREATE TABLE [accounts] (
  [id_accounts] int IDENTITY (1,1) NOT NULL, 
	[id_account_types] int NOT NULL, 
	[id_currencies] nvarchar(3) NOT NULL, 
	[id_icons] int DEFAULT(0) NOT NULL, 
	[account_name] nvarchar(100) NOT NULL, 
	[account_details] nvarchar(4000) NOT NULL, 
	[total_income_entries] int DEFAULT (0) NOT NULL, 
	[total_expense_entries] int DEFAULT (0) NOT NULL, 
	[last_operation] datetime NULL, 
	[date_created] datetime DEFAULT (GETDATE()) NOT NULL
);
GO
ALTER TABLE [accounts] ADD CONSTRAINT [PK_accounts] PRIMARY KEY ([id_accounts]);
GO
CREATE TABLE [account_types] (
  [id_account_types] int IDENTITY (1,1) NOT NULL, 
	[id_icons] int DEFAULT(0) NOT NULL,
	[account_type_name] nvarchar(50) NOT NULL
);
GO
ALTER TABLE [account_types] ADD CONSTRAINT [PK_account_types] PRIMARY KEY ([id_account_types]);
GO
CREATE TABLE [categories] (
  [id_categories] int IDENTITY (1,1) NOT NULL, 
	[id_icons] int DEFAULT(0) NOT NULL, 
	[parent_id] int DEFAULT (0) NOT NULL, 
	[category_type] tinyint DEFAULT (0) NOT NULL, 
	[category_name] nvarchar(100) NOT NULL, 
	[fore_color] int NOT NULL, 
	[back_color] int NOT NULL, 
	[font_style] tinyint NOT NULL, 
	[total_entries] int NOT NULL, 
	[last_operation] datetime NULL, 
	[date_created] datetime DEFAULT (getdate()) NOT NULL
);
GO
ALTER TABLE [categories] ADD CONSTRAINT [PK_categories] PRIMARY KEY ([id_categories]);
GO
CREATE TABLE [currencies] (
  [id_currencies] nvarchar(3) NOT NULL, 
	[id_icons] int DEFAULT(0) NOT NULL, 
	[long_name] nvarchar(50) NOT NULL, 
	[short_name] nvarchar(10) NOT NULL, 
	[priority] int DEFAULT (0) NOT NULL
);
GO
ALTER TABLE [currencies] ADD CONSTRAINT [PK_currencies] PRIMARY KEY ([id_currencies]);
GO
CREATE TABLE [items] (
  [id_items] int IDENTITY (1,1) NOT NULL, 
	[id_categories] int NOT NULL, 
	[id_accounts] int NOT NULL, 
	[id_icons] int DEFAULT(0) NOT NULL, 
	[entry_type] tinyint NOT NULL, 
	[title] nvarchar(100) NOT NULL, 
	[description] nvarchar(3800) NOT NULL, 
	[amount] money NOT NULL, 
	[date_entry] datetime NOT NULL,
	[date_updated] datetime NULL, 
	[date_created] datetime NOT NULL
);
GO
ALTER TABLE [items] ADD CONSTRAINT [PK_items] PRIMARY KEY ([id_items]);
GO
CREATE TABLE [icons] (
  [id_icons] int IDENTITY (1,1) NOT NULL, 
	[hash] uniqueidentifier NOT NULL,
	[data] varbinary(2048) NOT NULL,
	[date_created] datetime DEFAULT (getdate()) NOT NULL
);
GO
ALTER TABLE [icons] ADD CONSTRAINT [PK_icons] PRIMARY KEY ([id_icons]);
GO
CREATE TABLE [info] (
  [id_info] smallint NOT NULL, 
	[value] nvarchar(30) NOT NULL
);
GO
ALTER TABLE [info] ADD CONSTRAINT [PK_info] PRIMARY KEY ([id_info]);
GO
INSERT INTO [info] ([id_info], [value]) VALUES (8, '1.0'); -- сейчас номер схемы бд всегда 1.0
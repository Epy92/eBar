create database eBar;
use eBar;

create table UserTypes
(
	UserTypeId int not null primary key identity(1,1),
	TypeName nvarchar(100) not null,
);

create table UserTbl
(
	UserID int not null primary key identity(1,1),
	Username nvarchar(100) not null,
	UserPassword nvarchar(100) not null,
	Email nvarchar(200) not null,
	Name nvarchar(500) not null,
	UserPreferredLanguage nvarchar(10) null,
	UserTypeId int not null foreign key references UserTypes(UserTypeId)
);

create table Restaurants
(
	RestaurantId int not null primary key identity(1,1),
	RestaurantName nvarchar(255) not null,
);

create table RestaurantLocations
(
	LocationId int not null primary key identity(1,1),
	RestaurantCounty nvarchar(200) not null,
	RestaurantCity nvarchar(200) not null,
	RestaurantAddress nvarchar(300) null,
	Latitude decimal(10, 6) not null,
	Longitude decimal(10,6) not null,
	RestaurantId int not null foreign key references Restaurants(RestaurantId)
);

create table RestaurantAdministrators
(
	RestaurantAdminId int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	UserID int not null foreign key references UserTbl(UserID),
);

create table RestaurantTables
(
	TableId int not null primary key identity(1,1),
	TableNumber int not null,
	TableBarcode nvarchar(100) not null,
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
);

create table RestaurantTypes
(
	TypeId int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	TypeName nvarchar(250) not null,
);

create table RestaurantProductsCategories
(
	CategoryId int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	CategoryName nvarchar(250) not null,
);

create table RestaurantProducts
(
	ProductId int not null primary key identity(1,1),
	CategoryId int not null foreign key references RestaurantProductsCategories(CategoryId),
	ProductName nvarchar(250) not null,
	ProductPrice decimal(10,6) not null,
	ProductMeasurement nvarchar(100) not null,
	ProductMeasurementValue int,
	ProductMadeOf nvarchar(2000),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
);

create table UserResetPasswordCodes
(
	UserResetPwdId int not null primary key identity(1,1),
	UserID int not null foreign key references UserTbl(UserID),
	ResetCode nvarchar(10) not null,
	CreationDate datetime not null,
);

create table Languages
(
	LanguageId int not null primary key identity(1,1),
	LanguageCode nvarchar(5) not null,
	LanguageCountry nvarchar(100) not null,
	LanguageName nvarchar(100) null
);

create table RestaurantDetails
(
	RestaurantDetailsId int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	RestaurantDirectoryGuid nvarchar(100) null,
	RestaurantThumbnail nvarchar(max),
	RestaurantDescription nvarchar(3000) null,
	RestaurantContact nvarchar(1000),
	RestaurantProgram nvarchar(1000),
);

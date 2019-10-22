-- create database eBar;
-- use eBar;

create table UserTypes
(
	UserTypeId int not null primary key identity(1,1),
	TypeName nvarchar(100) not null,
);

create table UserDetails
(
	UserID int not null primary key identity(1,1),
	Username nvarchar(100) not null,
	Email nvarchar(200) not null,
	FirstName nvarchar(300) not null,
	LastName nvarchar(300) not null,
	BirthDate datetime null,
	FacebookToken nvarchar(max) null,
	FacebookPhotoUrl nvarchar(1000) null,
	GoogleToken nvarchar(max) null,
	GooglePhotoUrl nvarchar(1000) null,
	UserTypeId int not null foreign key references UserTypes(UserTypeId)
);

create table Restaurants
(
	RestaurantId int not null primary key identity(1,1),
	RestaurantName nvarchar(255) not null,
	RestaurantOwner int not null foreign key references UserTypes(UserTypeId)
);

create table RestaurantDetails
(
	RestaurantDetailsId int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	RestaurantDirectoryGuid nvarchar(100) null,
	RestaurantThumbnail nvarchar(max),
	RestaurantDescription nvarchar(3000) null,
	RestaurantPhone nvarchar(1000),
	RestaurantCity nvarchar(200) not null,
	RestaurantAddress nvarchar(300) null,
	Latitude decimal(10, 6) not null,
	Longitude decimal(10,6) not null,
	RestaurantProgram nvarchar(1000),
);

create table RestaurantTypes
(
	TypeId int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	TypeName nvarchar(250) not null,
);

create table RestaurantEvent
(
	RestaurantEventID int not null primary key identity(1,1),
	EventName nvarchar (500)  not null,
	EventDescription nvarchar (2000) not null,
	EventOffer  nvarchar (500)  null,
	EventPhotoUrl nvarchar(1000) null,
	EventStartDate datetime not null,
	EventEndDate datetime not null,
	EventPublicationDate  datetime not null,
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
);

create table Languages
(
	LanguageId int not null primary key identity(1,1),
	LanguageCode nvarchar(5) not null,
	LanguageCountry nvarchar(100) not null,
	LanguageName nvarchar(100) null
);

create table RestaurantFavorites
(
	RestaurantFavoriteID int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	UserID int not null foreign key references UserTbl(UserID),
);

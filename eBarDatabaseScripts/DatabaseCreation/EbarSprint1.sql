create table RestaurantReview
(
	RestaurantReviewID int not null primary key identity(1,1),
	UserID int not null foreign key references UserTbl(UserID),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	ReviewComment nvarchar(1000),
);

create table RestaurantEvent
(
	RestaurantEventID int not null primary key identity(1,1),
	EventTitle nvarchar (500)  not null,
	EventDescription nvarchar (2000) not null,
	EventStartDate datetime not null,
	EventEndDate datetime not null,
	EventPublicationDate  datetime not null,
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
);

create table RestaurantFavorite
(
	RestaurantFavoriteID int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	UserID int not null foreign key references UserTbl(UserID),
);
alter table RestaurantDetails
add RestaurantRating DECIMAL(5,2) default 0;

create table RestaurantGrades
(
	RestaurantGradeID int not null primary key identity(1,1),
	UserID int not null foreign key references UserTbl(UserID),
	RestaurantId int not null foreign key references Restaurants(RestaurantId) on delete cascade,
	Grade int not null default 0,
);
create table RestaurantReview
(
	RestaurantReviewID int not null primary key identity(1,1),
	UserID int not null foreign key references UserTbl(UserID),
	RestaurantId int not null foreign key references Restaurants(RestaurantId),
	Review nvarchar(1000) not null,
	Grade int not null default 0,
);

create table RestaurantEvent
(
	RestaurantEventID int not null primary key identity(1,1),
	EventTitle nvarchar (500)  not null,
	EventDescription nvarchar (2000) not null,
	EventStartDate datetime not null,
	EventEndDate datetime not null,
	EventPublicationDate  datetime not null,
	RestaurantId int not null foreign key references Restaurants(RestaurantId),
);

create table RestaurantFavorite
(
	RestaurantFavoriteID int not null primary key identity(1,1),
	RestaurantId int not null foreign key references Restaurants(RestaurantId),
	UserID int not null foreign key references UserTbl(UserID),
);
alter table RestaurantDetails
add RestaurantRating DECIMAL(5,2);


insert into RestaurantReview values(1,1,'Cel mai dragut restaurant',5);
insert into RestaurantReview values(2,1,'Chelnerul nu a fost cel mai ospitalier, insa mancarea a fost foarte buna',4);
insert into RestaurantReview values(3,1,'Mancarea a fost foarte buna','');
insert into RestaurantReview values(2,1,'Mancarea a fost oribila',1);
update RestaurantReview set Grade = 4 where RestaurantReviewID = 6;
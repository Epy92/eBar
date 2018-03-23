insert into UserTypes values('RegularUser');
insert into UserTypes values('RestaurantAdministrator');
insert into UserTypes values('RestaurantEmployee');

insert into UserTbl values('epy92', '123456', 'catalinepingeac@gmail.com', 'Catalin Epingeac', (select UserTypeId from UserTypes where TypeName = 'RestaurantAdministrator'));
insert into UserTbl values('rocco93', '123456', 'sebastian.dan93@yahoo.com', 'Dan Sebastian', (select UserTypeId from UserTypes where TypeName = 'RestaurantAdministrator'));
insert into UserTbl values('emma93', '123456', 'emiliavilceanu93@gmail.com', 'Emilia Vilceanu', (select UserTypeId from UserTypes where TypeName = 'RestaurantAdministrator'));

insert into Languages values('ro', 'Romania', 'Romanian');
insert into Languages values('en', 'United States', 'English');
insert into Languages values('en', 'United Kingdom', 'English');

---------------------------// Restaurant eBar data \\-----------------------------------------
declare @restaurantId int;
insert into Restaurants values('Restaurant eBar');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Restaurant traditional romanesc');
insert into RestaurantLocations values('Craiova', '44.318144',' 23.799422',@restaurantId);
insert into RestaurantTables values(1,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',1)),@restaurantId);
insert into RestaurantTables values(2,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',2)),@restaurantId);
insert into RestaurantTables values(3,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',3)),@restaurantId);
insert into RestaurantTables values(4,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',4)),@restaurantId);
insert into RestaurantTables values(5,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',5)),@restaurantId);
insert into RestaurantTables values(6,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',6)),@restaurantId);
insert into RestaurantTables values(7,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',7)),@restaurantId);
insert into RestaurantTables values(8,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',8)),@restaurantId);
insert into RestaurantTables values(9,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',9)),@restaurantId);
insert into RestaurantTables values(10,CONVERT(varchar,CONCAT(@restaurantId,'_Masa_',10)),@restaurantId);

declare @categoryId int;
insert into RestaurantProductsCategories values(@restaurantId, 'Aperitive reci');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Piept de pui afumat', 14, '1 portie / 100 gr', 100, 'Piept de pui', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Jambon de porc', 14, '1 portie / 100 gr', 100, 'Porc', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Zacusca', 14, '1 portie / 100 gr', 100, 'Ardei, Rosii', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata Boeuf', 16, '1 portie / 100 gr', 100, 'Cartofi fierti, Mazare, Castraveti murati, Masline, Maioneza, Piept pui', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata vinete', 14, '1 portie / 100 gr', 100, 'Vinete', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Icre de stiuca', 18, '1 portie / 100 gr', 100, 'Icre', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Fasole batuta', 15, '1 portie / 100 gr', 100, 'Fasole', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Tartar de somon proaspat cu avocado', 40, '1 portie / 200 gr', 200, 'Somon, avocado', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ansoa marinata', 18, '1 portie / 150 gr', 200, 'Ansoa', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Aperitive calde');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Mititei', 5, '1 bucata / 80 gr', 80, 'Carne de mici', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Bulete de cascaval', 14, '1 portie / 100 gr', 100, 'Cascaval', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Carnaciori oltenesti', 18, '1 portie / 100 gr', 100, 'Carnati', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Bulz ciobanesc', 25, '1 portie / 350 gr', 350, 'Mamaliga, branza, oua', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Foi de orez', 28, '1 portie / 150/100/30 gr', 280, 'Cu branza de capra, ciuperci si reductie de aceto balsamico alaturi de trio de salate', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ficat de rata la gratar', 60, '1 portion / 150/50 gr', 200, 'Ficat, mere verzi caramelizate si reductie de aceto balsamico', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Salate');
select @categoryId=SCOPE_IDENTITY();
--TO DO: continue



insert into RestaurantProductsCategories values(@restaurantId, 'Ciorbe/Supe');
insert into RestaurantProductsCategories values(@restaurantId, 'Paste');
insert into RestaurantProductsCategories values(@restaurantId, 'Pizza');
insert into RestaurantProductsCategories values(@restaurantId, 'Mancaruri gatite');
insert into RestaurantProductsCategories values(@restaurantId, 'Preparate la gratar');
insert into RestaurantProductsCategories values(@restaurantId, 'Garnituri');
insert into RestaurantProductsCategories values(@restaurantId, 'Sosuri');
insert into RestaurantProductsCategories values(@restaurantId, 'Vinuri');
insert into RestaurantProductsCategories values(@restaurantId, 'Bauturi racoritoare');
insert into RestaurantProductsCategories values(@restaurantId, 'Cocktailuri');
insert into RestaurantProductsCategories values(@restaurantId, 'Cafea, Ceai & Ciocolata Calda');
insert into RestaurantProductsCategories values(@restaurantId, 'Fresh & Limonada');




insert into Restaurants values('Restaurant OK');
insert into Restaurants values('Restaurant Tresor');
insert into Restaurants values('Restaurant Adrival');
insert into Restaurants values('Street Cafe');
insert into Restaurants values('Zvon Cafe');
insert into Restaurants values('Restaurant Carul cu Flori');


insert into RestaurantTypes values(2, 'Restaurant traditional romanesc');
insert into RestaurantTypes values(3, 'Restaurant italian');
insert into RestaurantTypes values(4, 'Pizzerie');
insert into RestaurantTypes values(5, 'Cafenea');
insert into RestaurantTypes values(6, 'Cafenea');



insert into RestaurantAdministrators values (1,1);
insert into RestaurantAdministrators values (1,2)
insert into RestaurantAdministrators values (1,3);



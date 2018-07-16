insert into UserTypes values('RegularUser');
insert into UserTypes values('RestaurantAdministrator');
insert into UserTypes values('RestaurantEmployee');

insert into UserTbl values('epy92', '123456', 'catalinepingeac@gmail.com', 'Catalin Epingeac', 'ro',(select top 1 UserTypeId from UserTypes where TypeName = 'RestaurantAdministrator'));
insert into UserTbl values('rocco93', '123456', 'sebastian.dan93@yahoo.com', 'Dan Sebastian', 'ro', (select top 1 UserTypeId from UserTypes where TypeName = 'RestaurantAdministrator'));
insert into UserTbl values('emma93', '123456', 'emiliavilceanu93@gmail.com', 'Emilia Vilceanu', 'ro', (select top 1 UserTypeId from UserTypes where TypeName = 'RestaurantAdministrator'));

insert into Languages values('ro', 'Romania', 'Romanian');
insert into Languages values('en', 'United States', 'English');
insert into Languages values('en', 'United Kingdom', 'English');

---------------------------// Restaurant eBar data \\----------------------------------------

declare @restaurantId int;
insert into Restaurants values('Restaurant eBar');
select @restaurantId=@@identity;
insert into RestaurantTypes values(@restaurantId, 'Restaurant traditional romanesc');
insert into RestaurantLocations (RestaurantCity, Latitude, Longitude, RestaurantId, RestaurantAddress, RestaurantCounty)
values('Craiova','44.318144',' 23.799422',@restaurantId, 'Strada eBar Service, 25', 'Dolj');

--insert into RestaurantLocations values('Craiova','Dolj','44.318144',' 23.799422',@restaurantId, 'Calea Bucuresti, 46A');

insert into RestaurantAdministrators values(@restaurantId, (select UserID from UserTbl where Username = 'epy92'));
insert into RestaurantAdministrators values(@restaurantId, (select UserID from UserTbl where Username = 'rocco93'));
insert into RestaurantAdministrators values(@restaurantId, (select UserID from UserTbl where Username = 'emma93'));

insert into RestaurantTables values(1,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',1, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(2,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',2, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(3,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',3, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(4,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',4, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(5,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',5, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(6,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',6, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(7,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',7, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(8,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',8, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(9,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',9, '#_#', LEFT(NEWID(),5))),@restaurantId);
insert into RestaurantTables values(10,CONVERT(nvarchar,CONCAT(@restaurantId,'#_#',10, '#_#', LEFT(NEWID(),5))),@restaurantId);

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
insert into RestaurantProducts values (@categoryId, 'Salata Caesar', 18, '1 portie / 300 gr', 300, 'Piept de pui, sos de salata Caesar clasic, parmezan, crutoane', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata Greceasca', 16, '1 portie / 250 gr', 250, 'Cu castraveti, rosii, ardei, feta, capere, masline si ulei de masline', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata picanta cu muschi de vaca', 20, '1 portie / 200/70 gr', 270, 'Muschi de vaca, ardei iute', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata din piept de rata', 25, '1 portie / 250/50 gr', 300, 'Piept de rata, sos de portocale si muguri de pin', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata Nicoise', 17, '1 portie / 250 gr', 250, 'Ton in ulei, fasole verde, masline, rosii cherry', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata de spanac proaspat cu branza de calda de capra', 22, '1 portie / 250 gr', 250, 'Spanac, branza de capra', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata de cartofi', 14, '1 portie / 250 gr', 250, 'Salata, sardina marinata', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata de varza', 10, '1 portie / 200 gr', 200, 'Varza', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata de rosii', 14, '1 portie / 250 gr', 250, 'Rosii, castraveti, branza, ceapa, ardei', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Ciorbe/Supe');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Supa de gaina', 10, '1 portie / 300/50 gr', 300, 'Cu taitei de casa', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciorba de perisoare', 12, '1 portie / 250 gr', 250, 'Cu rosii de gradina', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Bors de legume', 12, '1 portie / 200/70 gr', 270, 'Cu pipotele de rata, legume', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciorba de pui ardeleneasca', 12, '1 portie / 250/50 gr', 300, 'Cu tarhon proaspat si smantana', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciorba taraneasca de vacuta', 14, '1 portie / 250 gr', 250, 'Legume, smantana', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciorba de burta', 10, '1 portie / 250 gr', 250, 'Cu smantana, ujdei de usturoi si ardei iute', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Bors de peste', 14, '1 portie / 250 gr', 250, 'Cu leustean, telina si ceapa verde', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Supa zilei', 10, '1 portie / 250 gr', 200, 'Intrebati ospatarul', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciorba de fasole cu afumatura', 14, '1 portie / 250 gr', 250, 'Fasole, costita afumata', @restaurantId);


insert into RestaurantProductsCategories values(@restaurantId, 'Paste');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Primavera', 16, '1 portie / 300 gr', 300, 'Pene, ciuperci, legume', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Carbonara', 17, '1 portie / 300 gr', 300, 'Ou, smantana, bacon afumat, mozarella', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Amatriciana', 18, '1 portie / 300 gr', 300, 'Spaghete, sos rosii', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Qrabiatta', 17, '1 portie / 300 gr', 300, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Quatro Formaggi', 20, '1 portie / 300 gr', 300, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Tagliatele al Salmone', 24, '1 portie / 300 gr', 300, 'Tagliatele, somon', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Tagliatele cu trufe', 25, '1 portie / 300 gr', 300, 'Tagliatele, trufe', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Risotto cu ciuperci', 20, '1 portie / 300 gr', 300, 'Orez, ciuperci', @restaurantId);


insert into RestaurantProductsCategories values(@restaurantId, 'Pizza');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Pizza Margherita', 15, '1 pizza', 500, 'Sos pizza, mozzarella', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Prosciutto', 16, '1 pizza', 650, 'sos pizza, mozzarella, sunca', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Prosciutto e Funghi', 18, '1 pizza', 650, 'sos pizza, mozzarella, sunca, ciuperci', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Quatro Stagioni', 17, '1 pizza', 600, 'sos pizza, mozzarella, sunca, ciuperci, salam, ardei', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Quatro Formaggi', 20, '1 pizza', 600, 'mozzarella, gorgonzola, brie, parmezan', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Canibale', 20, '1 pizza', 650, 'sos pizza, mozzarella, sunca, salam, bacon, pastrama vita', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Diavola', 18, '1 pizza', 550, 'sos pizza, mozzarella, salam picant (chorizo)', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Salmone', 22, '1 pizza', 600, 'sos pizza, mozzarella, somon afumat, rucola, parmezan', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Calzone', 17, '1 pizza', 600, 'sos pizza, mozzarella, sunca, ciuperci, salam, sos separat', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Vegetariana', 15, '1 pizza', 550, 'sos pizza, mozzarella, ciuperci, ardei, ceapa, rosii, masline, broccoli, porumb', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Family', 25, '1 pizza', 750, 'sos pizza, mozzarella, salam, sunca, bacon, ciuperci, ardei, masline, porumb', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pizza Caprese', 18, '1 pizza', 550, 'mozzarella bocconcino, rosii chery, ulei masline, busoioc verde', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Mancaruri gatite');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Paste din grau dur la cuptor, cu pui, sos de rosii si toping de branza', 15, '1 pizza', 500, 'Sos pizza, mozzarella', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sarmalute in foi de varza murata', 16, '1 pizza', 650, 'sos pizza, mozzarella, sunca', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Julienne din muschi de vaca la tigaie', 18, '1 pizza', 650, 'sos pizza, mozzarella, sunca, ciuperci', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Tochitura moldoveneasca', 17, '1 pizza', 600, 'sos pizza, mozzarella, sunca, ciuperci, salam, ardei', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Snitel de vitel', 20, '1 pizza', 600, 'mozzarella, gorgonzola, brie, parmezan', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cotlet de porc pane', 20, '1 pizza', 650, 'sos pizza, mozzarella, sunca, salam, bacon, pastrama vita', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Piept de pui la gratar', 18, '1 pizza', 550, 'sos pizza, mozzarella, salam picant (chorizo)', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Rata rumenita la cuptor', 22, '1 pizza', 600, 'sos pizza, mozzarella, somon afumat, rucola, parmezan', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Pui de curte la ceaun', 17, '1 pizza', 600, 'sos pizza, mozzarella, sunca, ciuperci, salam, sos separat', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Preparate la gratar');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Piept de pui', 17, '1 portie / 250 gr', 250, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ceafa de porc', 18, '1 portie / 300 gr', 300, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Carnaciori la gratar', 18, '1 portie/ 250 gr', 250, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cotlet de porc', 18, '1 portie / 300 gr', 300, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Coaste de porc marinate', 20, '1 porite / 400 gr', 400, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cotlete fragede de berbecut', 24, '1 portie / 400 gr', 400, '', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Garnituri');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Cartof copt', 8, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cartofi prajiti', 7, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Piure de cartofi', 7, '1 portie/ 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Crochete de dovlecei', 12, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Legume sote', 12, '1 porite / 159 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Orez salbati', 16, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Broccoli', 12, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Salata mixta', 14, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Varza calita', 12, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Legume la gratar', 16, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciuperci la gratar', 14, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cartofi wedges', 8, '1 portie / 150 gr', 150, '', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Sosuri');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Bernaise', 8, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sos din hrean', 7, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sos de branza', 7, '1 portie/ 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Smantana', 12, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sos de ciuperci', 12, '1 porite / 159 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sos de piper verde', 16, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sos BBQ', 12, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sos chili', 14, '1 portie / 150 gr', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Mujdei', 12, '1 portie / 150 gr', 150, '', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Vinuri');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Castel Huniade', 45, '1 sticla', 750, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'La Cetate', 98, '1 sticle / 0.75', 750, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sauvignon Blanc', 56, '1 sticla / 0.75', 750, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Sauvignon Blanc', 13, '1 pahar/ 150 ml', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Rose, Casa Doina', 13, '1 pahar/ 150 ml', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Rose, Caloian', 15, '1 pahar/ 150 ml', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Feteasca neagra, Casa Doina', 13, '1 pahar/ 150 ml', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cabernet Sauvignon', 35, '1 sticla / 0.75', 750, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Vin la carafa', 25, '1 carafa / 1L', 1000, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Vin Tohani', 56, '1 sticla / 0.75', 750, 'Princiar Special Reserve Feteascã Neagrã, Pinot Noir, Cabernet Sauvignon', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Vinuri Jidvei', 38, '1 sticla/ 0.75', 750, 'Sauvignion Blanc sec, Feteascã Regalã sec', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Bere');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Tuborg', 9, 'ml', 250, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Tuborg fara alcool', 9, 'ml',500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Bere la halba', 7, 'ml', 500, 'Ursus, Tuborg, Carlsberg', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Carlsberg', 9, 'ml', 500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Holstein', 14, 'ml', 500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Timisoreana', 9, 'ml', 500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Becks', 9, 'ml', 500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Guinness', 14, 'ml', 500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Raddler', 8, 'ml', 500, '', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Bauturi racoritoare');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Pepsi Cola', 7.5, 'L', 0.25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Mirinda', 7, 'L', 0.25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, '7 UP', 7, 'L', 0.5, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Mountain Dew', 7, 'L', 0.25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Lipton Ice Tea', 8.5, 'L', 0.25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Prigat', 9, 'L', 0.25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Apa Minerala / Plata', 6, 'L', 0.5, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Red Bull', 12, 'L', 0.25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Burn', 10, 'L', 0.25, '', @restaurantId);


insert into RestaurantProductsCategories values(@restaurantId, 'Cocktailuri');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Aperol spritz | 11%', 28, 'cl', 25, 'Aperol, sparkling vine and sprkling water', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Gin Tonic | 40%', 20, 'cl', 25, 'Gin , apa tonica', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Campari Mix | 25%', 20, 'cl', 5, 'Campari and orange', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Vodka Mix | 40%', 22, 'cl', 25, 'Vodka and orange, cranberry , aple or tonic', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Bloody Mary | 40%', 20, 'cl', 25, 'Vodka, tomato juice and spices', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cuba Libre | 40%', 24, 'cl', 25, 'Rum, Coca-Cola, lemon', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Margarita | 38%', 28, 'cl', 50, 'Tequilla, triple sec, salt and lime juice', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cosmopolitan | 40%', 24, 'cl', 25, 'Vodka, Triple sec, lime juice and cranberry syrup', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Mojito | 40%', 24, 'cl', 25, 'Rum, mint, sugar syrup, lime, soda and crush ice', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Cafea, Ceai & Ciocolata Calda');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Ceai', 9, 'ml', 150, 'Verde, Menta, Fructe', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Espresso', 9, 'ml', 50, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Ciocolata calda', 12, 'ml', 100, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Machiatto', 20, 'ml', 25, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cappucino', 12, 'ml', 50, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cafe Latte', 14, 'ml', 100, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Espresso Double', 14, 'ml', 50, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Cafe Frappe', 16, 'ml', 150, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Irish coffee', 20, 'ml', 100, '', @restaurantId);

insert into RestaurantProductsCategories values(@restaurantId, 'Fresh & Limonada');
select @categoryId=SCOPE_IDENTITY();
insert into RestaurantProducts values (@categoryId, 'Fresh', 16, 'ml', 250, 'Orange, Grapefruit, Mixed', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Limonada clasica', 15, 'ml',500, '', @restaurantId);
insert into RestaurantProducts values (@categoryId, 'Limonada cu menta', 18, 'ml', 500, '', @restaurantId);


---------------------------// Restaurant OK data \\-----------------------------------------
insert into Restaurants values('Restaurant OK');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Restaurant traditional romanesc');

insert into Restaurants values('Restaurant Tresor');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Restaurant italian');

insert into Restaurants values('Restaurant Adrival');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Restaurant italian');

insert into Restaurants values('Street Cafe');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Cafenea');
insert into Restaurants values('Zvon Cafe');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Cafenea');

insert into Restaurants values('Restaurant Carul cu Flori');
select @restaurantId=SCOPE_IDENTITY();
insert into RestaurantTypes values(@restaurantId, 'Restaurant traditional romanesc');



insert into RestaurantAdministrators values (1,1);
insert into RestaurantAdministrators values (1,2)
insert into RestaurantAdministrators values (1,3);



CREATE TRIGGER TriggerAfterUpdateRestaurantGrades
ON RestaurantGrades
AFTER UPDATE
AS
Begin
declare @RestaurantId int  = (Select RestaurantId from inserted)
    update dbo.RestaurantDetails  set RestaurantRating =(Select
	cast(sum( IsNull(Grade,0)) as decimal(5,2)) / Cast(count(RestaurantGradeID) as decimal(5,2)) as Rating
	from dbo.[RestaurantGrades]
	where RestaurantId = @RestaurantId and (Grade BETWEEN 1 and 5 ) ) where RestaurantId = @RestaurantId;
End
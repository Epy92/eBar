CREATE TRIGGER TriggerAfterUpdateRestaurantReview
ON RestaurantReview
AFTER UPDATE
AS
Begin
declare @RestaurantId int  = (Select RestaurantId from inserted)
    update dbo.RestaurantDetails  set RestaurantRating =(Select
	cast(sum( IsNull(Grade,0)) as decimal(5,2)) / Cast(count(RestaurantReviewID) as decimal(5,2)) as Rating
	from dbo.[RestaurantReview]
	where RestaurantId = @RestaurantId and (Grade BETWEEN 1 and 5 ) ) where RestaurantId = @RestaurantId;
End
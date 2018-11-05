$(document).ready(function () {
    $("#startRestaurantConfig").on("click", function () {
        $.ajax({
            url: '/Restaurant/RestaurantDetails?restaurantId=0&pageMode=EditMode',
            type: 'GET',
            success: function (response) {
                
            },
            error: function () {
                
            }
        });
    });
});
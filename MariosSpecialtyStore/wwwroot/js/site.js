$(document).ready(function() {
    $('.add-review-form').submit(function(event) {
        event.preventDefault();
        $.ajax({
           type: 'GET',
           data: $(this).serialize(),
           url: '/Reviews/Create',
           success: function(result) {
                $('.add-review-button').hide();
                $('.review-output').html(result);

                $('.new-review').submit(function(event) {
                event.preventDefault();
                $.ajax({
                    url: '/Reviews/Create',
                    type: 'POST',
                    data: $(this).serialize(),
                    dataType: 'json',
                    success: function(result) {
                        var resultMessage = `<div class="card card-review">
                    <div class="card-body">
                        <h5 class="card-title">Rating: ${result.rating}</h5>
                        <small class="card-subtitle">Submitted By: ${result.author}</small>
                        <p class="card-text">${result.content_Body}</p>
                        ${if (result.loggedIn) {
                            `<small><a asp-controller="Reviews" asp-action="Edit" asp-route-id="@review.ReviewId">Edit</a> | <a asp-controller="Reviews" asp-action="Delete" asp-route-id="@review.ReviewId">Delete</a></small>`
                        }}
                    </div>
                </div>`;
                        $('.review-created-output').html(resultMessage);
                        $('.add-review-button').show();
                        $('.review-output').html('');
                    }
                });
            });
           } 
        });
    });


});
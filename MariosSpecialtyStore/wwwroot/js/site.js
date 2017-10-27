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
                        var resultMessage ="i hope this worked " + result.content_Body;
                        $('.review-created-output').html(resultMessage);
                    }
                });
            });
           } 
        });
    });


});
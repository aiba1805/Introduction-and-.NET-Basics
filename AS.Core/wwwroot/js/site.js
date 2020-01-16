$(document).ready(function () {
    $(".remove").click(function () {
        let button = $(this);
        let user = $(this).closest(".user");
        let userid = user.data("id");
        let url = "user/" + userid +"/delete-json";
        
        $.post(url).done(function (answerFromServer) {
            if (answerFromServer) {
                user.remove();
            }
        });
    });

    $(".createUser").click(function () {
        $(".createUserContainer").css("display", "block");
    });

    $(".createUserContainer").find(".createUserBtn").click(function (e) {
        e.preventDefault();

        let url = "/create-user-json";
        var form = $('#user-form')[0];
        
        var data = new FormData(form);

        $.ajax({
            type: "POST",
            enctype: 'multipart/form-data',
            url: url,
            data: data,
            processData: false,
            contentType: false,
            cache: false,
            timeout: 600000,
            success: function (data) {
                $(".createUserContainer").css("display", "none");
            },
            error: function (e) {
            }
        });
           /* .done(function (data) {
            debugger;
            $("#userList").append(`<tr class="user" data-id="${data.id}"><td>${data.firstName}</td><td>${data.lastName}</td><td>${data.age}</td><td><div class="rounded"><img src="${data.photoPath}" class="img-thumbnail" alt="" width="250" height="200"></div></td><td>${data.rewards}<div class="col-sm-12"><a asp-action="Details" asp-controller="Reward" asp-route-id="@reward.id" data-toggle="tooltip" data-placement="top" title="@reward.title"><div class="rounded mx-auto"><img src="@Url.Content(@System.IO.Path.Combine("Images/",@System.IO.Path.GetFileName(@reward.imagePath)))" class="img-thumbnail" alt="" width="50" height="50"></div></a></div>}</td><td><a asp-action="AddReward" asp-route-id="${data.id}" class="btn btn-outline-primary">Add Reward</a> |<a asp-action="Edit" asp-route-id="${data.id}" class="btn btn-outline-primary">Edit</a> |<a asp-action="Edit" asp-controller="Roles" asp-route-userid="${data.id}" class="btn btn-outline-primary">Edit role</a> |<a asp-action="Details" asp-route-id="${data.id}" class="btn btn-outline-primary">Details</a> |<a class="remove btn btn-outline-danger">Delete</a></td></tr>`)
        });*/

    });
});
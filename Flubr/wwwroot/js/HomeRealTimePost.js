$(() => {


    
    var connection = new signalR.HubConnectionBuilder().withUrl("/SignalHub").build();
    connection.start().then(function () {
        LoadPost();
    });
    connection.on("LoadData", function (pass) {
        console.log(pass);
        LoadPost();
    })

    

    


    function LoadPost() {

        var pl = '';
        

        $.ajax({

            url: '/Home/GetIndex/',
            method: 'GET',
            success: (result) => {
                $.each(result, (k, v) => {

                    const post = v.Post;
                    //let regex = /https:[/]*[a-z]*[.]*[a-zA-Z0-9]*[.]*[a-z]*/g;
                    //let newStr = ""
                    //let match = regex.exec(post);
                    //if (regex.test(post)){

                    //    newStr = post.replace(regex, `<a href="${match}"> ${match} </a>`)
                    //} else {

                    //    newStr = post;
                    //}


                    //this is a regular expression for capturing anything that's a link and setting it up as a clickable link.

                    //let newStr = post.replace(/(https:\/\/[^\s]+)/g, '<a href="$1">$1</a>');

                    
                    //this is a regular expression for capturing anything that's a link and a hashtag and setting them both up as a clickable link.
                    let newStr = post.replace(/(https:\/\/[^\s]+)|(#[^\s]+)/g, (match, p1, p2) => {
                        if (p1) {
                            return `<a href="${p1}">${p1}</a>`;
                        }
                        if (p2) {
                            return `<a href="https://twitter.com/search?q=${p2}">${p2}</a>`;
                        }
                    });

                    //console.log(result);

                    pl += `
                                <div class="contain"style="padding: 30px; padding-top: 10px; padding-bottom: 50px; margin-top: 10px; border: 1px solid #CDCDCD; box-shadow: 5px 1px 10px #888888; ">
                                    <img src="${v.ProfileImage}" alt="Girl in a jacket" width="55" height="55" style="margin-left: -15px; margin-right: 10px;">
                                    <div class=" comment" style="text-indent: 0px; text-indent: 0px;">
                                        <h8 style="text-align: left; "><strong>${v.FirstName} ${v.LastName}</strong></h8>  <a asp-controller="Profile" asp-action="Account" asp-route-id="${v.Id}" ><h8>@${v.UserName}</h8></a>
                                        <br/>
                                        <p1 style=" overflow:hidden;"> <small>${newStr} </small></p1>
                                   </div>
                                   <div class="icons" style="margin-bottom: -40px; padding-top: 20px; text-align: center;">
                                        <a asp-area="" asp-controller="Home" asp-action="Index"><i class="bi bi-chat-right-text" style="margin-right: 150px; color: black;"></i></a>
                                        <a asp-area="" asp-controller="Home" asp-action="Index"><i class="fa-solid fa-retweet" style="margin-right: 150px;  color: black;"></i></a>
                                        <a asp-area="" asp-controller="Home" asp-action="Index"><i class="fa-solid fa-heart" style="margin-right: 50px; color: black;"></i></a>
                                   </div>
                                </div>`

                })
                $("#homePost").html(pl);
            },
            error: (error) => {
                console.log(error)
            }
        });










    }
})

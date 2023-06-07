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
                                < div class="card mb-3" >
                                    <div class="card-body">
                                      <!-- Data -->
                                      <div class="d-flex mb-3">
                                        <a href="">
                                            <div class="rounded-circle overflow-hidden border" style="width: 40px; height: 40px;">
                                                <img src="${v.ProfileImage}" class="w-100 h-100 object-fit-cover" alt="" />
                                            </div>
                                        </a>
                                        <div class="ml-2">
                                          <a href="" class="text-dark mb-0">
                                            <strong>${v.FirstName} ${v.LastName}</strong>
                                          </a>
                                          <a href="" class="text-muted d-block" style="margin-top: -6px">
                                            <small>${v.UserName}</small>
                                          </a>
                                        </div>
                                      </div>
                                      <!-- Description -->
                                      <div>
                                        <p>
                                          ${newStr}
                                        </p>
                                      </div>
                                    </div>
                                    <!--Media -->
                                   < !-- < div class="bg-image hover-overlay ripple rounded-0" data - mdb - ripple - color="light" >
                                      <img src="https://mdbootstrap.com/img/new/standard/people/077.jpg" class="w-100" />
                                      <a href="#!">
                                        <div class="mask" style="background-color: rgba(251, 251, 251, 0.2)"></div>
                                      </a>
                                    </div > -->
                                    < !--Media -->
                                    < !--Interactions -->
                                    <div class="card-body">
                                      <!-- Reactions -->
                                      <div class="d-flex justify-content-between mb-3">
                                        <div>
                                          <a href="">
                                            <i class="fas fa-thumbs-up text-primary"></i>
                                            <i class="fas fa-heart text-danger"></i>
                                            <span>2</span>
                                          </a>
                                        </div>
                                        <div>
                                          <a href="" class="text-muted"> 8 comments </a>
                                        </div>
                                      </div>
                                      <!-- Reactions -->

                                      <!-- Buttons -->
                                      <div class="d-flex justify-content-between text-center border-top border-bottom mb-4">
                                        <form method="post" class="inline-form" asp-controller="Profile" asp-action="AddLike" asp-route-id="@item.PostId">
                                            <button type="submit" class="btn btn-link btn-lg" data-mdb-ripple-color="dark" asp-for="@item.PostId">
                                            <i class="fas fa-thumbs-up me-2"></i>Like
                                          </button>
                                        </form>

                                        <button type="button" class="btn btn-link btn-lg" data-mdb-ripple-color="dark">
                                          <i class="fas fa-comment-alt me-2"></i>Comment
                                        </button>
                                        <button type="button" class="btn btn-link btn-lg" data-mdb-ripple-color="dark">
                                          <i class="fas fa-share me-2"></i>Share
                                        </button>
                                      </div>
                                      <!-- Buttons -->

                                      <!-- Comments -->

                                      <!-- Input -->
                                      <div class="d-flex mb-3">
                                        <a href="">
                                          <img src="https://mdbootstrap.com/img/new/avatars/18.jpg" class="border rounded-circle me-2"
                                            alt="" style="height: 40px" />
                                        </a>
                                        <div class="form-outline w-100">
                                          <textarea class="form-control" id="textAreaExample" rows="2"></textarea>
                                          <label class="form-label" for="textAreaExample">Write a comment</label>
                                        </div>
                                      </div>
                                      <!-- Input -->

                                      <!-- Answers -->

                                      <!-- Single answer -->
                                      <div class="d-flex mb-3">
                                        <a href="">
                                          <img src="https://mdbootstrap.com/img/new/avatars/8.jpg" class="border rounded-circle me-2" alt=""
                                            style="height: 40px" />
                                        </a>
                                        <div>
                                          <div class="bg-light rounded-3 px-3 py-1">
                                            <a href="" class="text-dark mb-0">
                                              <strong>Malcolm Dosh</strong>
                                            </a>
                                            <a href="" class="text-muted d-block">
                                              <small>Lorem ipsum dolor sit amet consectetur,
                                                adipisicing elit. Natus, aspernatur!</small>
                                            </a>
                                          </div>
                                          <a href="" class="text-muted small ms-3 me-2"><strong>Like</strong></a>
                                          <a href="" class="text-muted small me-2"><strong>Reply</strong></a>
                                        </div>
                                   </div>

                                      <!-- Answers -->

                                      <!-- Comments -->
                                   </div>
                                    <!--Interactions -->
                                  </div >




                        `

                })
                $("#homePost").html(pl);
            },
            error: (error) => {
                console.log(error)
            }
        });










    }
})



    
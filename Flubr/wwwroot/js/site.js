////$(() => {

    
////    var connection = new signalR.HubConnectionBuilder().withUrl("/SignalHub").build();
////    connection.start();
////    connection.on("LoadData", function () {

////        LoadPost();
////    })
////    LoadPost();

////    function LoadPost(value) {

////        var pl = '';
        

////        $.ajax({

////            url: '/Messages/GetOpen/',
////            method: 'GET',
////            success: (result) => {
////                $.each(result, (k, v) => {

////                    pl += `<li class="me">
////                                <div class="entete">
////                                    <span class="status green"></span>
////                                    <h2>${v.applicationUser.FirstName}</h2>
////                                    <h3>{v.UM.Date}</h3>
////                                </div>
////                                <div class="triangle"></div>
////                                <div class="message">
////                                    ${v.Message}
////                                </div>
////                           </li>`

////                })
////                $("#chat").html(pl);
////            },
////            error: (error) => {
////                console.log(error)
////            }
////        });










////    }
////})              

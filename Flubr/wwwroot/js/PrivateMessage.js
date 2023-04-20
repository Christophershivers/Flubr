const connection = new signalR.HubConnectionBuilder().withUrl("/SignalHub").build();

$(function () {
    connection.start().then(function () {
        alert('connected');
        var url = window.location.href;
        var id = url.split('/').pop();
        InvokePriMess(id);
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

function InvokePriMess(value) {

    connection.invoke("PrivateMessage", value).catch(function (err) {

        return console.log(err.toString());
    });
}

connection.on("PriMess", function (privateMessages) {

    GetTheMessages(privateMessages);

});


function GetTheMessages(mess) {

    if (!mess) {

        return;
    }

    $('#messageTable').empty();

    var pl = '';

    const list = document.getElementById("messageTable");

    $.each(mess, function (index, mess) {
        pl += `
                               <li class="me">
                                    <div class="entete">
                                        <span class="status green"></span>
                                        <h2>${mess.firstName}</h2>
                                        <h3>${mess.date}</h3>
                                    </div>
                                    <div class="triangle"></div>
                                    <div class="message">
                                        ${mess.message}
                                    </div>
                                </li>


`
    });
    $("#messageTable").html(pl);

}
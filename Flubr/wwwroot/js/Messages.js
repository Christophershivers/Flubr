const connection = new signalR.HubConnectionBuilder()
    .withUrl("/SignalHub")
    .build();



$(function () {
    connection.start().then(function () {
        alert('connected');
        var url = window.location.href;
        var id = url.split('/').pop();
        InvokeMess(id);
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

function InvokeMess(value) {

    connection.invoke("GetMessage", value).catch(function (err) {

        return console.log(err.toString());
    });
}

connection.on("GetMess", function (mess) {

    GetTheMessages(mess);

});


function GetTheMessages(mess) {

    if (!mess) {

        return;
    }

    $('#messageTable').empty();

    const list = document.getElementById("messageTable");

    $.each(mess, function (index, mess) {
        const li = document.createElement("li");
        li.innerHTML = `Name: ${mess.id} - message: ${mess.message}`;
        list.appendChild(li);


    });

}
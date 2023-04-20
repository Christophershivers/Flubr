

//var connection = new signalR.HubConnectionBuilder().withUrl("/SignalHub").build();





//connection.on("RecieveData", function (data) {
//    console.log(data);
//    $.each(data, function   (index, item) {
//        $("#messagesList").append("<li>" + item.id + "-" + item.message + "</li>");
//    });
//});



//document.getElementById("sendMessage").addEventListener("click", function (event) {

//    var id = document.getElementById("senderEmail").value;
//    var message = document.getElementById("chatMessage").value;

//    connection.send("Send", id, message);
//    connection.invoke("ListData");
//    event.preventDefault();

    

//})

//connection.start().then(function () {
//    connection.invoke("ListData");
//})




























const connection = new signalR.HubConnectionBuilder()
    .withUrl("/SignalHub")
    .build();

connection.on("RecieveData", data => {
    // Update the list with the new data from the database
    updateList(data);
});


document.getElementById("sendMessage").addEventListener("click", function (event) {

    var id = document.getElementById("senderEmail").value;
    var message = document.getElementById("chatMessage").value;

    var url = window.location.href;
    var param = url.split('/').pop();
    connection.send("Send", id, message);
    connection.invoke("ListData", param);
    event.preventDefault();



})




function updateList(data) {

    if (!data) {

        return;
    }

    $('#list').empty();

    // Clear the current list
    const list = document.getElementById("list");
    //list.innerHTML = "";

    // Add the new items to the list
    data.forEach(item => {
        const li = document.createElement("li");
        // Set the text of the list item to the values of the specified fields
        li.innerHTML = `Name: ${item.id} - message: ${item.message}`;
        list.appendChild(li);
    });
}


connection.start()
    .then(() => {
        console.log("Connected to SignalR hub for table updates");

        var url = window.location.href;
        var param = url.split('/').pop();
        try {
            connection.invoke("ListData", param)
                .then(data => {
                    updateList(data);
                });

        } catch (err) {


            console.error(err.message);
            console.error(err.stack);
        }
    })
    .catch(err => {
        console.error(err.toString());
    });





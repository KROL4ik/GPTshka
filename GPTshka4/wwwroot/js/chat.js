   const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7116/chat")
    .build();

    connection.on("ReceiveMessage", (userName, message) => {
        console.log(userName);
        console.log(message);
        });

        connection.start().then(()=>{
        connection.invoke("JoinChat", getUserName());
             })


    function getUserName() {
        var userName = document.getElementById('userName').textContent;
    return userName;
    }

document.getElementById("sendButton").addEventListener("click",
    function () {
        let message = document.getElementById("inputField").value;
        document.getElementById("inputField").value = "";
        connection.invoke("Send", message).catch (function (err) {
            return console.error(err.toString());
        });
        displayMessage(message);
    }
)

function displayMessage(message) {

    let chatBox = document.getElementById("chatBox");
    const node = document.createElement("div");
    node.setAttribute("class", "chat-message user-message");
    const textNode = document.createTextNode(message);
    node.appendChild(textNode);
    chatBox.appendChild(node);

}



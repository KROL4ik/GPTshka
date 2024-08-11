
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7116/chat")
    .build();

    connection.on("ReceiveMessage", (userName, message) => {
        displayMessage(message,"gpt-message");
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
        displayMessage(message,"user-message");
    }
)

function displayMessage(message,sender) {

    let chatBox = document.getElementById("chatBox");
    const node = document.createElement("div");
    node.setAttribute("class", "chat-message "+sender);
    const textNode = document.createTextNode(message);
    node.appendChild(textNode);
    chatBox.appendChild(node);
}



"use strict";
const joinChat = async (userName) => {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:7116/Chat")
        .build();

    try {
        await connection.start();
        await connection.invoke("JoinChat", userName)
    } catch (error) {
        console.log(error);
    }
}
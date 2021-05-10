var thinkgear = require('node-thinkgear-sockets');
const {
  Client,
  Server,
  Message

} = require('node-osc');
var moment = require('moment'); // require
const neatCsv = require('neat-csv');

const notebookClient = new Client('127.0.0.1', 5010);
// const maxClient = new Client('127.0.0.1', 7001);
function sendMess(mess) {
  console.log("logged");
  notebookClient.send(mess, () => {
    console.log("sent");
  });
}

const fs = require('fs')

var data;
fs.readFile('./asmus.csv', async (err, data) => {
  if (err) {
    console.error(err)
    return
  }
  // console.log(await neatCsv(data))
  data = await neatCsv(data);
  setInterval(myFunc, 1000, data);
})


var i = 0;

function myFunc(data) {
  console.log(`arg was => ${i}` + " " + data[i].Frame);
  var message = new Message('/mynddata');
  message.append(data[i].Attention);
  message.append(data[i].Frame);
  sendMess(message);

  i = i + 1;
}
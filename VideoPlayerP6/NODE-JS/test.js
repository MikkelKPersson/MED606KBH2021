var thinkgear = require('node-thinkgear-sockets');
const {
  Client,
  Server
} = require('node-osc');

// const createCsvWriter = require('csv-writer').createObjectCsvWriter;

console.log("STARTING...");

// var osc = require('osc-min');

const oscClient = new Client('127.0.0.1', 7002);
// const maxClient = new Client('127.0.0.1', 7001);

oscClient.send('/oscAddress', 1337, () => {
  // oscClient.close();
});

var client = thinkgear.createClient();

function sendMess(mess) {
  oscClient.send(mess, () => {
    // console.log("sent");
  });
  // max.send('/' + path, data, () => {
  //   console.log("sent");
  // });
}


client.on('data', function(data) {
  // var time = (new Data).toTimeString();
  console.log((new Data).toTimeString() + ': ' + JSON.stringify(data));
  if (data.eSense.attention) {
    console.log(data.eSense.attention);
  }
  // var message = new Message('/mynddata');
  // message.append(time);
  // message.append(data.eSense.attention);
  // message.append(data.eSense.meditation);
  // message.append(data.poorSignalLevel);
  // sendmess(message);
  // sendMess("attention", data.eSense.attention);
  // sendMess("meditation", data.eSense.meditation);
  // sendMess("signalLevel", data.poorSignalLevel);
  // yass();
});

client.connect();
var thinkgear = require('node-thinkgear-sockets');
const {
  Client,
  Server
} = require('node-osc');

var osc = require('osc-min');

const oscClient = new Client('127.0.0.1', 7002);
oscClient.send('/oscAddress', 1337, () => {
  // oscClient.close();
});

var client = thinkgear.createClient();

function sendMess(path, data) {
  oscClient.send('/' + path, data, () => {
    // console.log("sent");
  });
}
client.on('data', function(data, oscClient) {
  console.log((new Date).toISOString() + ': ' + JSON.stringify(data));
  if (data.eSense.attention) {
    console.log(data.eSense.attention);
  }
  sendMess("attention", data.eSense.attention);
  sendMess("meditation", data.eSense.meditation);
  sendMess("signalLevel", data.poorSignalLevel);
  // yass();
});

client.connect();
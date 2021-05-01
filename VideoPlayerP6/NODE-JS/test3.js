var thinkgear = require('node-thinkgear-sockets');
const {
  Client,
  Server,
  Message

} = require('node-osc');
var moment = require('moment'); // require

const oscClient = new Client('127.0.0.1', 7002);
const maxClient = new Client('127.0.0.1', 7001);

var client = thinkgear.createClient();
//
function sendMess(mess) {
  console.log("logged");
  oscClient.send(mess, () => {
    console.log("sent");
  });
  maxClient.send(mess, () => {
    console.log("sent");
  });
}

var client = thinkgear.createClient();

client.on('data', function(data) {
  var time = moment().format('h:mm:ss:ms');
  // time = time.toTimeString();
  console.log(time + ': ' + JSON.stringify(data));
  console.log("BOEH");

  var message = new Message('/mynddata');
  console.log("huh");

  message.append(time);
  message.append(data.eSense.attention);
  message.append(data.eSense.meditation);
  message.append(data.poorSignalLevel);
  message.append(data.eegPower.delta);
  message.append(data.eegPower.theta);
  message.append(data.eegPower.lowAlpha);
  message.append(data.eegPower.highAlpha);
  message.append(data.eegPower.lowBeta);
  message.append(data.eegPower.highBeta);
  message.append(data.eegPower.lowGamma);
  message.append(data.eegPower.highGamma);
  sendMess(message);
  console.log("END");
});

client.connect();
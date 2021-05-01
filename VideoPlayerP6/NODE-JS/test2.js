var thinkgear = require('node-thinkgear-sockets');

var client = thinkgear.createClient();

client.on('data',function(data){
console.log((new Date).toISOString() + ': ' + JSON.stringify(data));
});

client.connect();

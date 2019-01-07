var MongoClient = require('mongodb').MongoClient;
var url = "mongodb://localhost:27017/";

MongoClient.connect(url, function(err, db) {
  if (err) throw err;
  var dbo = db.db("game_data");
  dbo.collection("player_data").findOne({}, function(err, result) {
    if (err) throw err;
    console.log("\nHealth:" + result["health"]);
	console.log("\nAmmo:" + result["ammo"]);
    db.close();
  });
});
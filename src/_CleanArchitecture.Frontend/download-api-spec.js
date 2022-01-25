const fs = require('fs');

var request = require('sync-request');

var dir = './.tmp';

if (!fs.existsSync(dir)) {
  fs.mkdirSync(dir);
}

var res = request('GET', 'https://localhost:7254/swagger/v1/swagger.json');
fs.writeFileSync(`${dir}/swagger.json`, res.getBody());

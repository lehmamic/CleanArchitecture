const fs = require('fs');

var request = require('sync-request');

var dir = './';

if (!fs.existsSync(dir)) {
  fs.mkdirSync(dir);
}

var res = request('GET', 'https://localhost:7254/swagger/v1/swagger.json');
fs.writeFileSync(`${dir}/src/api-spec.openapi.json`, res.getBody());

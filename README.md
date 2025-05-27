# litedbcli

Command Line tool for managing LiteDB database.

# Usage

```bash
litedbcli "test.db" <password>

> show tables
Coils
PPL
Users
Total collections: 3

> select * from Coils limit 1 offset 4;

[{"_id":{"$oid":"682f57589a69140b1bc73ea9"},"lotNo":"8d39de94-31b6-4e8e-8cc2-e8b473189c65","coilNo":"000005","ProductDescription":"Pasteries and Biscuits","Timestamp_qr":{"$date":"2025-05-22T16:56:56.7330000Z"},"deviceId_qr":"DESKTOP-78Z3269","nWeight":0.8,"image":"data:image/gif;base64,R0lGODlhAQABAIAAAP///wAAACH5BAEAAAAALAAAAAABAAEAAAICRAOAOw==","tag":"M","Timestamp_nw":{"$date":"2025-05-24T11:55:04.1700000Z"},"pplId":{"$oid":"68334b625b82ba00aaa12db7"}}]

```
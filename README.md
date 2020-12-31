

# SMA Backup Service

SMA Backup Service is a worker service which is installed in windows services or linux Systemd to automate backups. SMA Backup Service is able to create backup and upload to online backup storages.

## Important
#### SMA Backup Service is currently under development and only supports SQL Server and MongoDb databases as backup source and FileSystem and Google drive as backup destination.

## SMABackup
### SMABackup.Service uses [SMABackup](https://github.com/saeedmaghdam/SMABackup) library in order to process backup config file, create backup and upload to desired destination.
### https://github.com/saeedmaghdam/SMABackup 

## Sample schedulesettings.json
```json
{
	"Daily": [
		{
			"Hour": "00",
			"Minute": "21"
		}
	]
}
```

## Sample backupsettings.json
```json
{
	"Source": [
		{
			"Type": "SqlServer",
			"Name": "ProjectA_SqlServer",
			"ServerName": ".",
			"Username": "sa",
			"Password": "123456",
			"DatabaseName": "projecta_db"
		},
		{
			"Type": "MongoDb",
			"Name": "ProjectB_MongoDb",
			"CollectionName": "projectb",
			"HostName": "localhost",
			"Username": "projectb",
			"Password": "123456"
		},
		{
			"Type": "MongoDb",
			"Name": "ProjectC_MongoDb",
			"CollectionName": "projectc",
			"HostName": "localhost"
		}
	],
		"Destination": [
		{
			"Type": "GoogleDrive",
			"Name": "MyAccount@gmail.com_GoogleDrive",
			"ClientId": "00000000000-00000000000000000000000000000000.apps.googleusercontent.com",
			"ClientSecret": "0000000000000000000000",
			"ApplicationName": "AppNameInGoogleDrive"
		}
	],
	"Backup": [
		{
			"Source": "ProjectA_SqlServer",
			"Destination": "MyAccount@gmail.com_GoogleDrive",
			"PostBackup": {
				"DeleteBackup": "True"
			}
		},
		{
			"Source": "ProjectB_MongoDb",
			"Destination": "MyAccount@gmail.com_GoogleDrive",
			"PostBackup": {
				"DeleteBackup": "True"
			}
		},
		{
			"Source": "ProjectC_MongoDb",
			"Destination": "MyAccount@gmail.com_GoogleDrive",
			"PostBackup": {
				"DeleteBackup": "True"
			}
		}
	]
}
```

## Features
* ### Backup source including: SQL Server, MongoDb
* ### Backup destination including: FileSystem, Google Drive

## Release History
* 1.0.0
	* Initialized SMA Backup Service

## Meta
Saeed Aghdam –  [Linkedin](https://www.linkedin.com/in/saeedmaghdam/)

Distributed under the MIT license. See  [`LICENSE`](https://raw.githubusercontent.com/saeedmaghdam/SMABackup.Service/master/LICENSE)  for more information.

[https://github.com/saeedmaghdam/](https://github.com/saeedmaghdam/)

## Contributing

1.  Fork it ([https://github.com/saeedmaghdam/SMABackup.Service/fork](https://github.com/saeedmaghdam/SMABackup.Service/fork))
    
2.  Create your feature branch (`git checkout -b feature/your-branch-name`)
    
3.  Commit your changes (`git commit -am 'Add a short message describes new feature'`)
    
4.  Push to the branch (`git push origin feature/your-branch-name`)
  
5.  Create a new Pull Request
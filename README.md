# EThaiShop Project Repository

## Connection string for Docker
"DefaultConnection": "Server=localhost,1433;Database=EThai;User Id=sa;Password=123;TrustServerCertificate=True",

## Connection string for SQL Server
"DefaultConnection": "Server=DESKTOP-VJID4KV;Database=EThai;User Id=sa;Password=123;TrustServerCertificate=True"

## Install SSL for Localhost
- Use the mkcert to install SSL: https://github.com/FiloSottile/mkcert
- To install SSL for window, choose Chocolatey to setup: https://docs.chocolatey.org/en-us/choco/setup/ 
- And then install mkcert on Window machine: `choco install mkcert`
- Follow the instruction on the mkcert to install SSL for client project
- Config Angular.json file to use SSL from mkcert

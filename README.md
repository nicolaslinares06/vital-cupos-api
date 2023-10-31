# vital-cupos-api



## Getting started

To make it easy for you to get started with GitLab, here's a list of recommended next steps.

Already a pro? Just edit this README.md and make it your own. Want to make it easy? [Use the template at the bottom](#editing-this-readme)!

## Add your files

- [ ] [Create](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#create-a-file) or [upload](https://docs.gitlab.com/ee/user/project/repository/web_editor.html#upload-a-file) files
- [ ] [Add files using the command line](https://docs.gitlab.com/ee/gitlab-basics/add-file.html#add-a-file-using-the-command-line) or push an existing Git repository with the following command:

```
cd existing_repo
git remote add origin https://gitlab.com/otic1/vital/project-vital-3.0/vital-cupos-api.git
git branch -M main
git push -uf origin main
```

## Integrate with your tools

- [ ] [Set up project integrations](https://gitlab.com/otic1/vital/project-vital-3.0/vital-cupos-api/-/settings/integrations)

## Collaborate with your team

- [ ] [Invite team members and collaborators](https://docs.gitlab.com/ee/user/project/members/)
- [ ] [Create a new merge request](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)
- [ ] [Automatically close issues from merge requests](https://docs.gitlab.com/ee/user/project/issues/managing_issues.html#closing-issues-automatically)
- [ ] [Enable merge request approvals](https://docs.gitlab.com/ee/user/project/merge_requests/approvals/)
- [ ] [Automatically merge when pipeline succeeds](https://docs.gitlab.com/ee/user/project/merge_requests/merge_when_pipeline_succeeds.html)

## Test and Deploy

Use the built-in continuous integration in GitLab.

- [ ] [Get started with GitLab CI/CD](https://docs.gitlab.com/ee/ci/quick_start/index.html)
- [ ] [Analyze your code for known vulnerabilities with Static Application Security Testing(SAST)](https://docs.gitlab.com/ee/user/application_security/sast/)
- [ ] [Deploy to Kubernetes, Amazon EC2, or Amazon ECS using Auto Deploy](https://docs.gitlab.com/ee/topics/autodevops/requirements.html)
- [ ] [Use pull-based deployments for improved Kubernetes management](https://docs.gitlab.com/ee/user/clusters/agent/)
- [ ] [Set up protected environments](https://docs.gitlab.com/ee/ci/environments/protected_environments.html)

***

# Editing this README

When you're ready to make this README your own, just edit this file and use the handy template below (or feel free to structure it however you want - this is just a starting point!). Thank you to [makeareadme.com](https://www.makeareadme.com/) for this template.

## Suggestions for a good README
Every project is different, so consider which of these sections apply to yours. The sections used in the template are suggestions for most open source projects. Also keep in mind that while a README can be too long and detailed, too long is better than too short. If you think your README is too long, consider utilizing another form of documentation rather than cutting out information.

## Name
Choose a self-explaining name for your project.

## Description
Let people know what your project can do specifically. Provide context and add a link to any reference visitors might be unfamiliar with. A list of Features or a Background subsection can also be added here. If there are alternatives to your project, this is a good place to list differentiating factors.

## Badges
On some READMEs, you may see small images that convey metadata, such as whether or not all the tests are passing for the project. You can use Shields to add some to your README. Many services also have instructions for adding a badge.

## Visuals
Depending on what you are making, it can be a good idea to include screenshots or even a video (you'll frequently see GIFs rather than actual videos). Tools like ttygif can help, but check out Asciinema for a more sophisticated method.

## Installation
Within a particular ecosystem, there may be a common way of installing things, such as using Yarn, NuGet, or Homebrew. However, consider the possibility that whoever is reading your README is a novice and would like more guidance. Listing specific steps helps remove ambiguity and gets people to using your project as quickly as possible. If it only runs in a specific context like a particular programming language version or operating system or has dependencies that have to be installed manually, also add a Requirements subsection.

## Usage
Use examples liberally, and show the expected output if you can. It's helpful to have inline the smallest example of usage that you can demonstrate, while providing links to more sophisticated examples if they are too long to reasonably include in the README.

## Support
Tell people where they can go to for help. It can be any combination of an issue tracker, a chat room, an email address, etc.

## Roadmap
If you have ideas for releases in the future, it is a good idea to list them in the README.

## Contributing
State if you are open to contributions and what your requirements are for accepting them.

For people who want to make changes to your project, it's helpful to have some documentation on how to get started. Perhaps there is a script that they should run or some environment variables that they need to set. Make these steps explicit. These instructions could also be useful to your future self.

You can also document commands to lint the code or run tests. These steps help to ensure high code quality and reduce the likelihood that the changes inadvertently break something. Having instructions for running tests is especially helpful if it requires external setup, such as starting a Selenium server for testing in a browser.

## Authors and acknowledgment
Show your appreciation to those who have contributed to the project.

## License
For open source projects, say how it is licensed.

## Project status
If you have run out of energy or time for your project, put a note at the top of the README saying that development has slowed down or stopped completely. Someone may choose to fork your project or volunteer to step in as a maintainer or owner, allowing your project to keep going. You can also make an explicit request for maintainers.

## Docker installation for backend 

1.We run or open our terminal to perform the following commands 
  Now we could try building the image:

  $ sudo docker build . -t cuposapi

  Name of our image: cuposapi 

2.Run it by exposing port 80 of the container to port 8081 of our machine:

  $ sudo docker run –name cuposapi -p 8081:80 -d cuposapi

3.Review the generation of the container as follows

  $ sudo docker ps

  Expected result to display the container:
    -Container ID
    -picture
    -Command
    -Created
    -Status
    -ports

4.We check in a browser as follows:
  
  We check in a browser as follows:

  Route: localhost:8081 or our IP:8081
  Port: 8081 the one indicated in the command made to expose our port in the container

Note: Required files in the repository
    -Dockerfile
    -.dockerignore
  
## Docker installation for database

1.We open the terminal to install and execute the Microsoft SQL server image for the execution of the DB with the following command:

  $ sudo docker pull mcr.microsoft.com/mssql/server:2017-lastest

2.We can validate the SQL installation with the following command:

  $ sudo docker images

  Expected result to display the image:
    -Repository
    -Tag
    -Image ID
    -Created
    -Size

  Official container images for Microsoft SQL Server on Linux for Docker Engine:
    -mcr.microsoft.com/mssql/server:2017-lastest

3.Start an instance of mssql-server for SQL Server 2017, which is now Generally Available ( GA ).
    
    $ docker run -d -p 1433:1433 --name MINAMBIENTE -e MSSQL_SA_PASSWORD=P@ssword -e ACCEPT_EULA=Y mcr.microsoft.com/mssql/server:2017-latest

    You can use environment variables to configure SQL Server on Linux containers.

    ACCEPT_EULA confirms your acceptance of the End User License Agreement.

    MSSQL_SA_PASSWORD is the database sysadmin password (userid = 'sa') used to connect to SQL Server once the container is running. Important Note: This password must include at least 8 characters from at least three of these four categories: uppercase letters, lowercase letters, numbers, and non-alphanumeric symbols.

    MSSQL_PIDes the product ID (PID) or edition with which the container will run. Acceptable values:

    Developer: This will run the container using Developer Edition (this is the default if an MSSQL_PID environment variable is not provided)
    Express: This will run the container using Express Edition
    Standard: This will run the container using the Standard Edition
    Enterprise: This will run the container using the Enterprise Edition
    EnterpriseCore: This will run the container using Enterprise Edition Core


Requirements
    This image requires Docker Engine 1.8+ on any of its supported platforms.

    At least 2 GB of RAM (3.25 GB before 2017-CU2). Make sure to allocate enough memory to the Docker VM if you are running Docker.

    Requires the following environment flags

    "ACCEPT_EULA=Y"

    "MSSQL_SA_PASSWORD=<your_strong_password>"

    "MSSQL_PID=<your_product_id | edition_name> (default: Developer)"

    A strong system administrator (SA) password: at least 8 characters, including uppercase letters, lowercase letters, base 10 digits, and/or non-alphanumeric symbols.

4.We validate using the following command for the creation of the container

    $ sudo docker ps

    Expected result to display the container:
        -Container ID
        -picture
        -Command
        -Created
        -Status
        -ports

5.Copy the .bak file into our SQL container
    Name: MINAMBIENTE_DB.bak
    Path: MSSQL/BAK/MINAMBIENTE_DB.bak

6.We carry out the following command with the location of our .bak file

    $ docker cp MINAMBIENTE_DB.bak 3c0c78b9254b:/var/opt/mssql/data



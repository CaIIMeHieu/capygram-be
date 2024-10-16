#!/bin/bash
source_auth="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Auth"
source_common="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Common"
source_graph="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Graph"
source_media="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Media"
source_notification="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Notification"
source_post="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Post"
source_newsfeed="/home/ubuntu/capygram/capygram-be/src/Services/capygram.Newsfeed"
source_ocelot="/home/ubuntu/capygram/capygram-be/src/ApiGateways/capygram.Ocelot"

# Di chuyển đến thư mục và chạy dotnet run
echo "####################################### checkout common project #########################################"
cd "$source_common"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project common ###########################################"

echo "####################################### checkout auth project ###########################################"
cd "$source_auth"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project auth #############################################"

echo "####################################### checkout graph project ##########################################"
cd "$source_graph"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project graph ############################################"

echo "####################################### checkout media project ##########################################"
cd "$source_media"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project media ############################################"

echo "####################################### checkout notification project ###################################"
cd "$source_notification"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project notification #####################################"

echo "####################################### checkout post project ###########################################"
cd "$source_post"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project post #############################################"

echo "####################################### checkout newsfeed project #######################################"
cd "$source_newsfeed"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish project newsfeed #########################################"

echo "####################################### checkout ocelot #######################################"
cd "$source_ocelot"
echo "####################################### run project #####################################################"
dotnet run &
echo "####################################### finish ocelot #########################################"
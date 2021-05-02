<?php
    $servename = $_POST["host"];
    $database = $_POST["database"];
    $srvr_user = $_POST["dbuser"];
    $srvr_pw = $_POST["dbpw"];

 
    $con = mysqli_connect($servename, $srvr_user, $srvr_pw, $database);
    if (mysqli_connect_errno())
    {
        echo("1: Connection failed");
        exit();
    }
    
    $friendList = $_POST["users"];

    $selectQuery1 = "SELECT user1 FROM `friendlist` WHERE user2 = '" .$friendList. "' AND FriendStatus = 1";
    $queryResult1 = mysqli_query($con,$selectQuery1);

   $selectQuery2 = "SELECT user2 FROM `friendlist` WHERE user1 = '" .$friendList. "' AND FriendStatus = 1";
    $queryResult2 = mysqli_query($con,$selectQuery2);
    
    if(mysqli_num_rows($queryResult1) < 1){
        echo("You have no friends nerd!");
        exit();
    }

    echo("0");
    while($row = mysqli_fetch_array($queryResult1))
	{
		$ret = "\n" .  $row["user1"];
        echo($ret);
	}

    while($row = mysqli_fetch_array($queryResult2))
	{
		$ret = "\n" .  $row["user2"];
        echo($ret);
	}

    mysqli_close($con);
?>
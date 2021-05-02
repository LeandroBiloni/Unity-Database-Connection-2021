<?php
	
	$servename = $_POST["host"];
    $database = $_POST["database"];
    $srvr_user = $_POST["dbuser"];
     $srvr_pw = $_POST["dbpw"];

 
  $con = mysqli_connect($servename, $srvr_user, $srvr_pw, $database);
  if (mysqli_connect_errno())
  {
    echo("1: Connection OK");
    exit();
  }
  
  $userA = $_POST["userA"];
  $userB = $_POST["userB"];

  $selectQuery = "SELECT * FROM `friendlist` WHERE user1 = ('" .$userA. "') AND user2 = ('" .$userB. "')";
  $selectQueryResult = mysqli_query($con,$selectQuery);

  $selectQuery1 = "SELECT * FROM `friendlist` WHERE user1 = ('" .$userB. "') AND user2 = ('" .$userA. "')";
  $selectQueryResult1 = mysqli_query($con,$selectQuery1);
  if(!mysqli_num_rows($selectQueryResult) != 0 && !mysqli_num_rows($selectQueryResult1) != 0){
	$insertQuery = "INSERT INTO `friendlist`(`user1`, `user2`, `FriendStatus`) VALUES ('".$userA."','".$userB."',0)"; //('.$userA.','.$userB.',0)
	mysqli_query($con,$insertQuery);
  }
  else{
	  echo("User Not Found");
  }
  
 

  echo("0");


  mysqli_close($con);

?>
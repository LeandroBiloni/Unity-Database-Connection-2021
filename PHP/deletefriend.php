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
  
  $userA = $_POST["userA"];
  $userB = $_POST["userB"];
  
  
  $selectQuery1 = "SELECT * FROM `friendlist` WHERE user1 = ('".$userA."') AND user2 = ('".$userB."')";
  $selectQueryResult1 = mysqli_query($con, $selectQuery1);
  $deleteQuery1 = "DELETE FROM `friendlist` WHERE user1 = ('".$userA."') AND user2 = ('".$userB."')";

  $selectQuery2 = "SELECT * FROM `friendlist` WHERE user1 = ('".$userB."') AND user2 = ('".$userA."')";
  $selectQueryResult2 = mysqli_query($con, $selectQuery2);
  $deleteQuery2 = "DELETE FROM `friendlist` WHERE user1 = ('".$userB."') AND user2 = ('".$userA."')";
  
  if(mysqli_num_rows($selectQueryResult1) != 0){
    mysqli_query($con, $deleteQuery1) or die($con->error);
  }
  else if(mysqli_num_rows($selectQueryResult2) != 0){
    mysqli_query($con, $deleteQuery2) or die($con->error);
  }
  else
     echo("2: Friend not found");

 
 
  
  echo("0");


  mysqli_close($con);

?>
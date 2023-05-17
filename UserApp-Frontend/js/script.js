document.getElementById("userForm").addEventListener("submit", function(event) {
  event.preventDefault();

  //var form = event.target;
  var name = document.getElementById("name").value;
  var email = document.getElementById("email").value;
  var phone = document.getElementById("phone").value;
  var address = document.getElementById("address").value;
  var picture = document.getElementById("profilePic").files[0];

  var data = new FormData();
  data.append("name", name);
  data.append("email", email);
  data.append("phone", phone);
  data.append("address", address);
  data.append("picture", picture);
 //var data = new FormData(form);

  fetch("https://localhost:7045/api/users", {
      method: "POST",
      body: data
  })
  .then(response => response.json())
  .then(user => {
    data.reset();
      loadUsers();
  })
  .catch(error => {
      console.error("Error:", error);
  });
});

function loadUsers() {
  fetch("https://localhost:7045/api/users")
  .then(response => response.json())
  .then(users => {
      var userList = document.getElementById("userList");
      userList.innerHTML = "";

      users.forEach(user => {
          var listItem = document.createElement("li");
          listItem.innerHTML = `
              <strong>Name:</strong> ${user.name}<br>
              <strong>Email:</strong> ${user.email}<br>
              <strong>Phone:</strong> ${user.phone || ""}<br>
              <strong>Address:</strong> ${user.address || ""}<br>
              <img src="https://localhost:7045/wwwroot/profileimages/${user.picture}" alt="Profile Picture" width="100"><br>
              <button onclick="editUser(${user.id})">Edit</button>
              <button onclick="deleteUser(${user.id})">Delete</button>
              <hr>
          `;
          userList.appendChild(listItem);
      });
  })
  .catch(error => {
      console.error("Error:", error);
  });
}

function editUser(id) {
  fetch(`https://localhost:7045/api/users/${id}`)
  .then(response => response.json())
  .then(user => {
      document.getElementById("name").value = user.name;
      document.getElementById("email").value = user.email;
      document.getElementById("phone").value = user.phone || "";
      document.getElementById("address").value = user.address || "";
      // Handle picture editing if needed
  })
  .catch(error => {
      console.error("Error:", error);
  });
}

function deleteUser(id) {
  if (confirm("Are you sure you want to delete this user?")) {
      fetch(`https://localhost:7045/api/users/${id}`, {
          method: "DELETE"
      })
      .then(() => {
          loadUsers();
      })
      .catch(error => {
          console.error("Error:", error);
      });
  }
}

// Load users on page load
loadUsers();

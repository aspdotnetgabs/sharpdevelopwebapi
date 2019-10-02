<template>
       <form @submit.prevent="login" autocomplete="off">
            <h2>Login</h2>
            <p>
                <label>Email</label>
                <input type="text" v-model="user.email" required />
            </p>
            <p>
                <label>Password</label>
                <input type="password" v-model="user.password" required />
            </p>
            <p>
                <button type="submit" :disabled="disabledBtn === 'btnLogin'" >Login</button>
                <a href="#" @click="display('registerform')">Register</a>
            </p>
        </form>
</template>

    <script>
        module.exports =  {
            props: ['baseurl'],             
            data: function () {
                return {
                    appUserStorageId: 'customerAppUser123',
                    user: {},
                    disabledBtn: "",
                    value: 'I am the child.'
                }
            },
            methods: {
                login: function () {
                    this.disabledBtn = "btnLogin";
                    axios.post(this.baseurl + "/TOKEN?email=" + this.user.email + "&password=" + this.user.password)
                        .then(response => {
                            var user = response.data;
                            // If response has user token...
                            if (user.Token) {
                                user.email = this.user.email;
                                localStorage.setItem(this.appUserStorageId, JSON.stringify(user)); // store User data in localStorage  
                                this.$emit('data-child', user); // Instead of calling the method we emit an event                                       
                            }
                        })
                        .catch(error => {
                            alert(error.response.data.Message);
                            this.disabledBtn = "";
                        });
                }
            },
            mounted: function () {
                console.log('Login component loaded.');
                console.log(this.baseurl);                
            }
        };

    </script> 

    <style scoped>

    </style>
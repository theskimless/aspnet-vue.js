function _(data) {
    console.log(data);
}

//HEADER NAV
var headerNav = new Vue({
    el: "#header-nav",
    data: {
        currentRoute: window.location.pathname,
        links: [
            { name: "Главная", url: "/Home/Index" },
            { name: "Блог", url: "/Blog/Index" }
        ]
    }
});

const months = ["Января", "Февраля", "Марта", "Апреля", "Мая", "Июня", "Июля", "Августа", "Сентября", "Октября", "Ноября", "Декабря"];
//BLOG PAGE
var blog = new Vue({
    el: "#blog",
    data: {
        rubrics: [],
        posts: [],
        postRubrics: [],
        selectedRubric: 0,
        rubricSelectsArr: [0]
    },
    methods: {
        addRubricSelect() {
            this.rubricSelectsArr.push(0);
        },
        hasSelectedRubric(postId) {
            //SHOW ALL POSTS
            if (this.selectedRubric === 0) return true;
            //SHOW IF HAS SELECTED RUBRIC
            for (var i = 0; i < this.postRubrics.length; i++) {
                if (this.postRubrics[i].PostId === postId && this.selectedRubric === this.postRubrics[i].RubricId) {
                    return true;
                }
            }
            return false;
        },
        getPostRubrics(postId) {
            var rubrics = "";
            for (var i = 0; i < this.postRubrics.length; i++) {
                if (this.postRubrics[i].PostId === postId) {
                    rubrics += " / " + this.rubrics[this.postRubrics[i].RubricId];
                }
            }
            return rubrics;
        },
        getJson() {
            axios.get("/Blog/SendJson")
                .then((response) => {
                    var data = response.data;
                    var posts = data.Posts;
                    //data.Rubrics = [0: {Id: 1, Name: "Text"}]
                    var rubrics = data.Rubrics.map(p => p.Name);
                    rubrics.unshift("Все");
                    //rubrics = [0: "Все", 1: "Text"]

                    //TRANSFORM DATE 1 Января, 2019
                    for (var key in posts) {
                        var newDate = new Date(posts[key].Date);
                        posts[key].Date = newDate.getDate() + " " + months[newDate.getMonth()] + ", " + newDate.getFullYear();
                    }

                    this.postRubrics = data.PostRubrics;
                    this.rubrics = rubrics;
                    //SHOW THE NEWEST POSTS IN THE TOP
                    this.posts = posts.slice().reverse();
                })
                .catch(function(err) {
                    _(err);
                });
        }
    },
    created() {
        this.getJson();
    }
});

//INDEX PAGE
Vue.component("slider", {
    props: ["slide"],
    template: `
        <img class="slide" :class="{'slide-active': slide.isActive}"  :src="slide.imgSrc" alt="">
    `
});
var app = new Vue({
    el: "#app",
    data: {
        lessonDay: "",
        lessonNum: 1,
        lessonCont: "",
        sliderIndex: 0,
        slides: [
            { imgSrc: "/imgs/slide1.jpg", isActive: true },
            { imgSrc: "/imgs/slide2.jpg", isActive: false }
        ],
        groupIndex: 0,
        groups: [
            {
                number: "121-17-1", content: {
                    monday: { 1: "monday1", 2: "monday2", 3: "monday3" },
                    tuesday: { 3: "tuesday3", 4: "tuesday4" },
                    wednesday: {},
                    thursday: {},
                    friday: {}
                }
            },
            {
                number: "122-17-1", content: {
                    monday: {},
                    tuesday: {},
                    wednesday: { 1: "wednesday1", 2: "wednesday2", 3: "wednesday3", 4: "wednesday4" },
                    thursday: { 2: "thursday2", 3: "thursday3" },
                    friday: {}
                }
            },
            {
                number: "122-17-2", content: {
                    monday: { 1: "monday1", 2: "monday2", 3: "monday3" },
                    tuesday: { 3: "tuesday3", 4: "tuesday4" },
                    wednesday: {},
                    thursday: {},
                    friday: {}
                }
            },
            {
                number: "122-17-3", content: {
                    monday: { 1: "monday1", 2: "monday2", 3: "monday3" },
                    tuesday: { 3: "tuesday3", 4: "tuesday4" },
                    wednesday: {},
                    thursday: {},
                    friday: {}
                }
            }
        ],
        group: ""
    },
    methods: {
        slideBtn: function (b) {
            this.slides[this.sliderIndex].isActive = false;
            if (b && this.sliderIndex < this.slides.length - 1) this.sliderIndex++;
            else if (!b && this.sliderIndex > 0) this.sliderIndex--;
            this.slides[this.sliderIndex].isActive = true;
        },
        dotBtn: function (index) {
            this.slides[this.sliderIndex].isActive = false;
            this.sliderIndex = index;
            this.slides[index].isActive = true;
        },
        selectGroup: function (group, index) {
            this.groupIndex = index;
            this.group = group;
        },
        getJson: function () {
            var test = this;
            axios.get("/Home/SendJson")
                .then((response) => {
                    this.groups[0] = response.data;
                    this.group = this.groups[0];
                })
                .catch(function (error) {
                    _(error);
                });
        },
        sendData() {
            axios.post("/Home/GetJson", this.groups)
                .then((response) => {
                    _(response);
                })
                .catch(function (err) {
                    _(err);
                });
        }
    },
    created() {
        this.group = this.groups[0];
        this.getJson();
    },
    watch: {
        lessonDay: getLesson,
        lessonNum: getLesson,
        lessonCont: function (val) {
            if (this.groups[this.groupIndex].content[this.lessonDay]) {
                if (val === "") {
                    this.$delete(this.groups[this.groupIndex].content[this.lessonDay], this.lessonNum);
                } else if (val !== undefined) {
                    this.groups[this.groupIndex].content[this.lessonDay][this.lessonNum] = val;
                }
            }
        }
    }
});
function getLesson() {
    this.lessonCont = this.groups[this.groupIndex].content[this.lessonDay] ? this.groups[this.groupIndex].content[this.lessonDay][this.lessonNum] : "No lesson";
}

//NAV ANCHORS
$("[nav-anchor]").on("click", function () {
    var id = $(this).attr("nav-anchor");
    $("html, body").animate({
        scrollTop: $("#" + id).offset().top
    }, 700);
});
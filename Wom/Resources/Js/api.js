wom = {

    getValue: function (param) {

    },

    getProject: function (projectId) {
        var model = {
            Id: 3,
            Title: "Test",
            ProjectUrl: "http://ravendb.net",
            Description: "Test is not a document database",
            Creator: {
                Id: 343,
                Name: "Rob"
            }
        };
        return model;
    },

    getPopularProjectComments: function (projectId, page, pageSize) {
        var model = [];
        model.push({
            ProjectId: 43,
            Score: 3,
            Functional: 'Warning',
            MonoVersion: '1.0',
            ProjectVersion: '3.4.3',
            User: {
                Id: 3,
                Name: "Rob"
            },
            Description: "This project sucks balls"
        });

        model.push({
            ProjectId: 43,
            Score: 1,
            Functional: 'Works',
            MonoVersion: '1.0',
            ProjectVersion: '3.4.3',
            User: {
                Id: 3,
                Name: "Rob"
            },
            Description: "This project is amazing"
        });
        return model;
    }
};
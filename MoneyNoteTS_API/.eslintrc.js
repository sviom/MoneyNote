module.exports = {
    root: true,
    env: {
        browser: true,
        node: true,
        es6: true,
    },
    parserOptions: {
        sourceType: 'module',
        parser: '@babel/eslint-parser',
        ecmaVersion: 2022,
    },
    extends: [
        // https://github.com/vuejs/eslint-plugin-vue#priority-a-essential-error-prevention
        // consider switching to `plugin:vue/strongly-recommended` or `plugin:vue/recommended` for stricter rules.
        // 'plugin:vue/essential',
        'eslint:recommended',
    ],
    // required to lint *.vue files
    // plugins: ['vue'],
    // add your custom rules here
    rules: {},
};
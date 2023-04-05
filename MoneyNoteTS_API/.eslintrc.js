module.exports = {
    root: true,
    env: {
        browser: true,
        node: true,
        es6: true,
    },
    parser: '@typescript-eslint/parser',
    parserOptions: {
        sourceType: 'module',
        parser: '@babel/eslint-parser',
        ecmaVersion: 2022,
    },
    extends: ['eslint:recommended', 'plugin:@typescript-eslint/recommended'],
    // required to lint *.vue files
    plugins: ['@typescript-eslint'],
    // add your custom rules here
    rules: {
        eqeqeq: ['warn', 'always'],
    },
};

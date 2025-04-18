const fs = require('fs');
const path = require('path');

const ICONS_DIR = path.resolve(__dirname, '../public/icons');
const OUTPUT_FILE = path.resolve(__dirname, '../src/app/shared/icons.enum.ts');

function toPascalCase(name) {
  return name
    .replace(/(-|_|\s)+(.)?/g, (_, __, chr) => (chr ? chr.toUpperCase() : ''))
    .replace(/^\w/, c => c.toUpperCase())
    .replace(/\.svg$/, '');
}

const files = fs.readdirSync(ICONS_DIR).filter(file => file.endsWith('.svg'));

const enumEntries = files.map(file => {
  const key = toPascalCase(file);
  return `  ${key} = '${file}'`;
});

const enumContent = `// AUTO-GENERATED FILE. DO NOT EDIT.

export enum Icons {
${enumEntries.join(',\n')}
}
`;

fs.writeFileSync(OUTPUT_FILE, enumContent);
console.log(`✅ Generated icons.enum.ts with ${files.length} entries.`);

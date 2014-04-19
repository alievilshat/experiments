<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:table('str_varianttypes')">
          <feature>
            <featureid m:left="-78" m:top="168">pk:feature(variant-<xsl:value-of select="id" />)</featureid>
            <name m:left="-76" m:top="203">
              <xsl:value-of select="name" />
            </name>
            <ispublic m:left="-80" m:top="270">1</ispublic>
          </feature>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>
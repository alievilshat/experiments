<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:user="http://www.navitas.nl/2014/Mapper/scripts">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select bpv.*, w.businessid from cms_blogpostvalue bpv inner join cms_blogpost bp on bp.postid = bpv.postid inner join cms_website w on w.websiteid = bp.websiteid')">
          <blogpost>
            <blogpostid m:left="-91" m:top="169" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="postvalueid" />
            </blogpostid>
            <languageid m:left="-92" m:top="198" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="languageid" />
            </languageid>
            <title m:left="32" m:top="214" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="title" />
            </title>
            <preview m:left="-91" m:top="230" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="preview" />
            </preview>
            <post m:left="-91" m:top="258" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="post" />
            </post>
            <postdate m:left="32" m:top="274" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="postdate" />
            </postdate>
            <url m:left="-92" m:top="290" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="url" />
            </url>
            <texturl m:left="32" m:top="305" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="texturl" />
            </texturl>
            <pagetitle m:left="-92" m:top="321" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="pagetitle" />
            </pagetitle>
            <metadescription m:left="33" m:top="337" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="metadescription" />
            </metadescription>
            <metakeywords m:left="-90" m:top="354" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="metakeywords" />
            </metakeywords>
            <tags m:left="33" m:top="370" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="tags" />
            </tags>
            <ispublished m:left="-91" m:top="385" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="ispublished" />
            </ispublished>
            <sequenceid m:left="32" m:top="401" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="sequenceid" />
            </sequenceid>
            <businessid m:left="33" m:top="182" xmlns:m="http://www.navitas.nl/2014/Mapper">
              <xsl:value-of select="businessid" />
            </businessid>
          </blogpost>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
  <msxsl:script implements-prefix="user" language="CSharp"><![CDATA[
  ]]></msxsl:script>
</xsl:stylesheet>